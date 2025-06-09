using BCrypt.Net;
namespace CryptoPortfolioTrackerAPI.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
            {
                return true;
            }
            return false;
        }
    }
}