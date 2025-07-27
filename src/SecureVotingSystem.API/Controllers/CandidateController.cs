using Microsoft.AspNetCore.Mvc;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Infrastructure.Data;

namespace SecureVotingSystem.API.Controllers;

[Route("api/candidate")]
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
        var result = await _candidateRepository.GetAll();
        return Ok(result);
    }
    
}