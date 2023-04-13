using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Service.Vote.Application.Votes.Commands.DeleteVote
{
    public sealed class DeleteVoteCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
