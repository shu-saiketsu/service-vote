using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Votes.Queries.GetElectionVotes
{
    public sealed class GetVotesQuery : IRequest<List<VoteEntity>?>
    {
        public int ElectionId { get; set; }
    }
}
