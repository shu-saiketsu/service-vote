using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Users.Queries.GetUser;

public sealed class GetUserQuery : IRequest<UserEntity?>
{
    public string Id { get; set; } = null!;
}