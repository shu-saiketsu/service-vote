using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandHandler : IRequestHandler<CreateElectionCommand, ElectionEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateElectionCommand> _validator;

    public CreateElectionCommandHandler(IApplicationDbContext context, IValidator<CreateElectionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<ElectionEntity?> Handle(CreateElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = new ElectionEntity
        {
            Id = request.Id
        };

        await _context.Elections.AddAsync(election, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return election;
    }
}