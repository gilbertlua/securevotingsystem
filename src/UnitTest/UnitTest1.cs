using SecureVotingSystem.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using SecureVotingSystem.Application.Interfaces;

namespace UnitTest;

public class Tests
{
    private IOtpService _simulatedOtpService;
    private IOtpGenerator _otpGenerator;
    [SetUp]
    public void Setup()
    { 
        var loggerFactory = new LoggerFactory();
        _simulatedOtpService = new SimulatedOtpService(loggerFactory.CreateLogger<SimulatedOtpService>());
        _otpGenerator = new OtpGenerator();
    }

    [Test]
    public async Task ShouldReturnTrueIfSuccessfullySent()
    {
        var otp = _otpGenerator.GenerateOtpCode();
        var send = await _simulatedOtpService.SendOtpAsync("082290382084", otp);
        Assert.True(send);
    }
}