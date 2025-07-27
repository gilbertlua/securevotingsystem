using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;
using SecureVotingSystem.Infrastructure.Services;

namespace UnitTest;

public class VotingRepositoryTest:IDisposable
{
    private ApplicationDbContext _context;
    private IVoterRepository _repository;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new VoterRepository(_context);
    }
    
    public void Dispose() => _context.Dispose();

    [Test]
    public void ShouldCreateNewVoting()
    {
        var voters = AddRecord();
        _repository = new VoterRepository(_context);
        _repository.Create(voters);
        Assert.That(_context.Voters.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldGetAllVoters()
    {
        var voters = AddRecord();
        _repository = new VoterRepository(_context);
        await _repository.Create(voters);
        var result = await _repository.GetAll();
        Assert.That(result.Count, Is.EqualTo(1));    
    }

    [Test]
    public async Task ShouldGetVoterByActivationCode()
    {
        var voters = AddRecord();
        _repository = new VoterRepository(_context);
        await _repository.Create(voters);
        var activationCode = _context.Voters.Single(x => x.ActivationCode == voters.ActivationCode);
        if (activationCode.ActivationCode == null)
        {
            throw new Exception("Activation code not found");
        }
        var actualActivationCode = await _repository.GetByActivationCodeAsync(activationCode.ActivationCode);
        Assert.That(actualActivationCode, Is.EqualTo(activationCode));
    }

    private Voter AddRecord()
    {
        var voter = new Voter
        {
            Id = 1,
            FullName = "John Doe",
            PhoneNumber = "555-555-5555",
        };
        return voter;
    }
    
}