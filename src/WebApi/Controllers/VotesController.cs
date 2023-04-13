using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Service.Vote.Application.Votes.Commands.CreateVote;
using Saiketsu.Service.Vote.Application.Votes.Queries.CalculateVote;

namespace Saiketsu.Service.Vote.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class VotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CastVote(CreateVoteCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == null) return BadRequest();

        return Ok(response);
    }

    [HttpGet("{electionId:int}/calculate")]
    public async Task<IActionResult> CalculateVotes(int electionId)
    {
        var query = new CalculateVoteQuery { ElectionId = electionId };

        var response = await _mediator.Send(query);

        if (response == null) return BadRequest();

        return Ok(response);
    }
}