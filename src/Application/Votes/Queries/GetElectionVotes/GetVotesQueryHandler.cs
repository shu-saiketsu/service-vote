using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Votes.Queries.GetElectionVotes
{
    public sealed class GetVotesQueryHandler : IRequestHandler<GetVotesQuery, List<VoteEntity>?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<GetVotesQuery> _validator;
        private readonly IMediator _mediator;

        public GetVotesQueryHandler(IApplicationDbContext context, IValidator<GetVotesQuery> validator, IMediator mediator)
        {
            _context = context;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<List<VoteEntity>?> Handle(GetVotesQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var query = new GetElectionQuery { Id = request.ElectionId };
            var election = await _mediator.Send(query, cancellationToken);

            if (election == null)
                return null;

            var votes = await _context.Votes.Where(x => x.ElectionId == request.ElectionId).ToListAsync(cancellationToken);

            return votes;
        }
    }
}
