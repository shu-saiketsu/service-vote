using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Votes.Queries.GetVote
{
    public sealed class GetVoteQuery : IRequest<VoteEntity?>
    {
        public int ElectionId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
