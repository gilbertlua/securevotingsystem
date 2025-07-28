using Microsoft.AspNetCore.Mvc;
using SecureVotingSystem.Application.Interfaces;

namespace SecureVotingSystem.API.Controllers;
[Route("api/election")]
[ApiController]
public class ElectionController : Controller
{
    private readonly ILogger<ElectionController> _logger;
    private readonly IElectionManager _electionManager;

    public ElectionController(ILogger<ElectionController> logger, IElectionManager electionManager)
    {
        _logger = logger;
        _electionManager = electionManager;
    }

    [HttpGet("get-all-elections")]
    public async Task<IActionResult> GetAllElections()
    {
        _logger.LogInformation("Getting all Elections");
        var result = await _electionManager.GetAll();
        return Ok(result);
    }

    [HttpPost("add-election")]
    public async Task<IActionResult> AddElection(string activationCode, int candidateId)
    {
        _logger.LogInformation("Adding Election");
        _logger.LogInformation("activationCode: {activationCode}, candidateId: {candidateId}",
            activationCode,
            candidateId);
        var result = await _electionManager.ExecutionSelector(activationCode, candidateId);
        return Ok(result);
    }
}