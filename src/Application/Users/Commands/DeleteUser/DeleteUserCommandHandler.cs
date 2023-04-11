using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Application.Users.Queries.GetUser;

namespace Saiketsu.Service.Vote.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<DeleteUserCommand> _validator;

    public DeleteUserCommandHandler(IApplicationDbContext context, IValidator<DeleteUserCommand> validator,
        IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var query = new GetUserQuery { Id = request.Id };
        var user = await _mediator.Send(query, cancellationToken);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}