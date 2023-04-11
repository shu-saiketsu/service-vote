using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Saiketsu.Service.Vote.Application;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Candidate;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Election;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.User;
using Saiketsu.Service.Vote.Domain.Options;
using Saiketsu.Service.Vote.Infrastructure;
using Saiketsu.Service.Vote.Infrastructure.Persistence;
using Serilog;
using Serilog.Events;

const string serviceName = "Vote";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ServiceName", serviceName)
    .WriteTo.Console()
    .CreateBootstrapLogger();

static void InjectSerilog(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ServiceName", serviceName)
        .WriteTo.Console());
}

static void SubscribeEventBus(IHost app)
{
    using var scope = app.Services.CreateScope();
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

    eventBus.Subscribe<UserCreatedIntegrationEvent>();
    eventBus.Subscribe<UserDeletedIntegrationEvent>();

    eventBus.Subscribe<ElectionCreatedIntegrationEvent>();
    eventBus.Subscribe<ElectionDeletedIntegrationEvent>();

    eventBus.Subscribe<CandidateCreatedIntegrationEvent>();
    eventBus.Subscribe<CandidateDeletedIntegrationEvent>();
}

static void PerformDataMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment()) context.Database.Migrate();
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(x => x.LowercaseUrls = true);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddHealthChecks();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Vote API",
            Description = ".NET Web API for managing Saiketsu votes."
        });

        options.EnableAnnotations();
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Vote.Application.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Vote.Domain.xml"));
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    builder.EnableRetryOnFailure();
                })
            .UseSnakeCaseNamingConvention();
    });

    builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(RabbitMQOptions.Position));

    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    builder.Services.AddSingleton<IEventBus, RabbitEventBus>();
}

static void AddMiddleware(WebApplication app)
{
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
    app.MapHealthChecks("/health");
}

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    InjectSerilog(builder);
    AddServices(builder);

    var app = builder.Build();

    AddMiddleware(app);
    SubscribeEventBus(app);
    PerformDataMigrations(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}