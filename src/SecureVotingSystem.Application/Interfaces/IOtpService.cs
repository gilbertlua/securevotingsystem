namespace SecureVotingSystem.Application.Interfaces;

public interface IOtpService
{
    /// <summary>
    /// This interface to send the OTP to the client
    /// </summary>
    /// <param name="phoneNumber">the phone number</param>
    /// <param name="otpCode">The OTP code</param>
    /// <returns></returns>
    Task<bool> SendOtpAsync(string phoneNumber, string otpCode);
}