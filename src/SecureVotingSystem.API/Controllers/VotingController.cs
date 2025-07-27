using Microsoft.AspNetCore.Mvc;
using SecureVotingSystem.Application.DTOs;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;


namespace SecureVotingSystem.API.Controllers;

[Route("api/voting")]
[ApiController]

public class VotingController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IVoterRepository _voterRepository;
    private readonly ILogger<VotingController> _logger;

    public VotingController(IVoterRepository voterRepository, ApplicationDbContext context, ILogger<VotingController> logger)
    {
        _context = context;
        _voterRepository = voterRepository;
        _logger = logger;
    }
    [HttpGet("get-all-votes")]
    public async Task<IActionResult> GetAllVotes()
    {
        var voters = await _voterRepository.GetAll();
        return Ok(voters);
    }

    [HttpPost("create-vote")]
    public async Task<IActionResult> CreateVote(VoterDto voter)
    {
        try
        {
            _logger.LogInformation("Begin to create voter .. ");
            var createVoter = new Voter
            {
                PhoneNumber = voter.PhoneNumber,
                FullName = voter.FullName,
            };
            var result = await _voterRepository.Create(createVoter);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(@"There was an error creating voter .. {e}", e.Message);
            return NotFound(@"Phone number already exists");
        }
    }

}