using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, CandidateEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetCandidateQuery> _validator;

    public GetCandidateQueryHandler(IApplicationDbContext context, IValidator<GetCandidateQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<CandidateEntity?> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidate = await _context.Candidates.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return candidate;
    }
}