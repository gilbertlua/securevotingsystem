using SecureVotingSystem.Application;
using SecureVotingSystem.Application.Interfaces;
using Microsoft.Extensions.Logging;
namespace SecureVotingSystem.Infrastructure.Services;

public class SimulatedOtpService : IOtpService
{
    private  ILogger<SimulatedOtpService> _logger;
    public SimulatedOtpService(ILogger<SimulatedOtpService> logger)
    {
        _logger = logger;
    }

    public Task<bool> SendOtpAsync(string phoneNumber, string otp)
    {
        // For development, just log the OTP instead of sending an SMS.
        _logger.LogInformation("--- SIMULATED OTP ---");
        _logger.LogInformation("Sending OTP {otp} to phone number {phoneNumber}", otp, phoneNumber);
        _logger.LogInformation("---------------------");
        return Task.FromResult(true); // Assume it was sent successfully.
    }
}