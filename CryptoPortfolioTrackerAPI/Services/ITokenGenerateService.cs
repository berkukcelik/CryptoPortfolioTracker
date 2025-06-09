using CryptoPortfolioTrackerAPI.Models;

namespace CryptoPortolioTrackerAPI.Services
{
    public interface ITokenGenerateService
    {
        string GenerateJwtToken(User user);
    }
}