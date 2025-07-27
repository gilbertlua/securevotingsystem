using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;
using SecureVotingSystem.Infrastructure.Services;


namespace SecureVotingSystem.API.Controllers;

[Route("api/voting")]
[ApiController]
public class VotingController : Controller
{
    private readonly ILogger<VotingController> _logger;

    public VotingController(ILogger<VotingController> logger)
    {
        _logger = logger;
    }
    
}