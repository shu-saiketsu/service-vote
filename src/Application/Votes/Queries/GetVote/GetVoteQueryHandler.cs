using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Votes.Queries.GetVote
{
    public sealed class GetVoteQueryHandler : IRequestHandler<GetVoteQuery, VoteEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<GetVoteQuery> _validator;

        public GetVoteQueryHandler(IApplicationDbContext context, IValidator<GetVoteQuery> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<VoteEntity?> Handle(GetVoteQuery request, CancellationToken cancellationToken)
        {
            var vote = await _context.Votes.SingleOrDefaultAsync(x => x.ElectionId == request.ElectionId && x.UserId == request.UserId, cancellationToken);

            return vote;
        }
    }
}
