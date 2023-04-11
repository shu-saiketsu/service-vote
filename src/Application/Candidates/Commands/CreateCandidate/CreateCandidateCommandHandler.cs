using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateCandidateCommand> _validator;

    public CreateCandidateCommandHandler(IApplicationDbContext context, IValidator<CreateCandidateCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<CandidateEntity?> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidate = new CandidateEntity
        {
            Id = request.Id
        };

        await _context.Candidates.AddAsync(candidate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return candidate;
    }
}