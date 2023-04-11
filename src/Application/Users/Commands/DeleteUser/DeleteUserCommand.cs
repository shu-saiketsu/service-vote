using MediatR;

namespace Saiketsu.Service.Vote.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}