using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Candidates.Queries.GetCandidate;
using Saiketsu.Service.Vote.Application.Common;

namespace Saiketsu.Service.Vote.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<DeleteCandidateCommand> _validator;

    public DeleteCandidateCommandHandler(IApplicationDbContext context, IValidator<DeleteCandidateCommand> validator,
        IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var query = new GetCandidateQuery { Id = request.Id };
        var candidate = await _mediator.Send(query, cancellationToken);

        if (candidate == null) return false;

        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}