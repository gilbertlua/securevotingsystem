using Microsoft.AspNetCore.Mvc;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Application.DTOs;

namespace SecureVotingSystem.API.Controllers;

[Route("api/candidate")]
[ApiController]
public class CandidateController : Controller
{
    private readonly ILogger<CandidateController> _logger;
    private readonly ICandidateRepository _candidateRepository;
    
    public CandidateController(ILogger<CandidateController> logger, ICandidateRepository  candidateRepository)
    {
        _logger = logger;
        _candidateRepository = candidateRepository;
    }

    [HttpGet("get-all-candidates")]
    public async Task<IActionResult> GetAllCandidates()
    {
        _logger.LogInformation("Getting all candidates ...");
        var result = await _candidateRepository.GetAll();
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCandidate(CandidateDto candidateDto)
    {
        _logger.LogInformation("Creating candidate ...");
        var result = await _candidateRepository.Create(candidateDto);
        return Ok(result);
    }
    
}