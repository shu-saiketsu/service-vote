using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Application.Elections.Queries.GetElection;

namespace Saiketsu.Service.Vote.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandHandler : IRequestHandler<DeleteElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<DeleteElectionCommand> _validator;

    public DeleteElectionCommandHandler(IApplicationDbContext context, IValidator<DeleteElectionCommand> validator,
        IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var query = new GetElectionQuery { Id = request.Id };
        var election = await _mediator.Send(query, cancellationToken);
        if (election == null) return false;

        _context.Elections.Remove(election);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}