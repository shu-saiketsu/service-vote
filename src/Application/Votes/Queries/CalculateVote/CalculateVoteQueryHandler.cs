using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Application.Votes.Queries.GetElectionVotes;

namespace Saiketsu.Service.Vote.Application.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQueryHandler : IRequestHandler<CalculateVoteQuery, Dictionary<int, int>?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<CalculateVoteQuery> _validator;
        private readonly IMediator _mediator;

        public CalculateVoteQueryHandler(IApplicationDbContext context, IValidator<CalculateVoteQuery> validator, IMediator mediator)
        {
            _context = context;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<Dictionary<int, int>?> Handle(CalculateVoteQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var query = new GetVotesQuery { ElectionId = request.ElectionId };
            var votes = await _mediator.Send(query, cancellationToken);
            if (votes == null) return null;

            var voteData = new Dictionary<int, int>();
            votes.ForEach(vote =>
            {
                if (!voteData.ContainsKey(vote.CandidateId))
                {
                    voteData.Add(vote.CandidateId, 1);
                }
                else
                {
                    voteData[vote.CandidateId] += 1;
                }
            });

            return voteData;
        }
    }
}
