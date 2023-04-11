using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommand : IRequest<UserEntity?>
{
    public string Id { get; set; } = null!;
}