using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Votes;

namespace Saiketsu.Service.Vote.Application.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, VoteEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<CreateVoteCommand> _validator;
        private readonly IEventBus _eventBus;

        public CreateVoteCommandHandler(IApplicationDbContext context, IValidator<CreateVoteCommand> validator, IEventBus eventBus)
        {
            _context = context;
            _validator = validator;
            _eventBus = eventBus;
        }

        public async Task<VoteEntity?> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var vote = new VoteEntity
            {
                CandidateId = request.CandidateId,
                ElectionId = request.ElectionId,
                UserId = request.UserId
            };

            await _context.Votes.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var @event = new VoteCastIntegrationEvent { ElectionId = request.ElectionId, UserId = request.UserId };
            _eventBus.Publish(@event);

            return vote;
        }
    }
}
