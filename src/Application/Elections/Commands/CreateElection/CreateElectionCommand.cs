using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommand : IRequest<ElectionEntity?>
{
    public int Id { get; set; }
}