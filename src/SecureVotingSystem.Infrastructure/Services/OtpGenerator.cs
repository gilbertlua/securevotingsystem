using System.Text;
using System.Security.Cryptography;
using SecureVotingSystem.Application.Interfaces;

namespace SecureVotingSystem.Infrastructure.Services;

public class OtpGenerator: IOtpGenerator
{
    public string GenerateOtpCode(int length = 6)
    {
        if (length < 6)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }
        const string validChars = "0123456789";
        var result = new StringBuilder(length);
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes); 
            foreach (byte b in randomBytes)
            {
                // The modulo operator gives us a random index within the bounds of our validChars string.
                // For "0123456789", the length is 10, so (b % 10) will result in a number from 0 to 9.
                result.Append(validChars[b % validChars.Length]);
            }
        }

        return result.ToString();
    }
}