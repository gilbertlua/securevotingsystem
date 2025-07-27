namespace SecureVotingSystem.Application.Interfaces;

public interface IOtpGenerator
{
    /// <summary>
    /// Generate otpCode
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    string GenerateOtpCode(int length = 6);
}