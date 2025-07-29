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
        try
        {
            await _electionManager.ExecutionSelector(activationCode, candidateId);
            return Ok("Record added successfully");
        }
        catch (InvalidOperationException  ex)
        {
            _logger.LogError("Vote Operation Valid{ex.Message}",ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("Vote Operation Invalid{e.Message}", e.Message);
            return StatusCode(500,"An error occured");
        }
        
    }

    [HttpGet("get-candidate-election")]
    public async Task<IActionResult> GetElectionCandidate(int candidateId)
    {
        _logger.LogInformation("Getting Election Candidate");
        try
        {
            _logger.LogInformation("Begin election candidate");
            var result = await _electionManager.GetByCandidateId(candidateId);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("Vote Operation Invalid{e.Message}", e.Message);
            throw;
        }
    }
}