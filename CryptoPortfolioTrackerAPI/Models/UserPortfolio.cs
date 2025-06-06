namespace CryptoPortfolioTrackerAPI.Models
{
    public class UserPortfolio
    {
        public int Id { get; set; }
        public User? User { get; set; } 
        public decimal TotalValue { get; set; }

        
        // default boş liste tanımladım. 
        public List<CryptoCoin> CryptoCoins { get; set; } = new List<CryptoCoin>();
        
        
    }
}