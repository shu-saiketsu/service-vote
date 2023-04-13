using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Application.Votes.Queries.GetVote;

namespace Saiketsu.Service.Vote.Application.Votes.Commands.DeleteVote
{
    public sealed class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<DeleteVoteCommand> _validator;
        private readonly IMediator _mediator;

        public DeleteVoteCommandHandler(IApplicationDbContext context, IValidator<DeleteVoteCommand> validator, IMediator mediator)
        {
            _context = context;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var query = new GetVoteQuery { ElectionId = request.ElectionId, UserId = request.UserId };

            var vote = await _mediator.Send(query, cancellationToken);

            if (vote == null) return false;

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
