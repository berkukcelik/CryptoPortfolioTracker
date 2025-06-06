namespace CryptoPortfolioTrackerAPI.Models
{
    public class CryptoCoin
    {
        public int Id { get; set; }
        public int ApiId { get; set; }

        public string Name { get; set; } = null!;

        public string Symbol { get; set; } = null!;

        public decimal CurrentPrice { get; set; }

        public int UserPortfolioId { get; set; }

        public UserPortfolio? UserPortfolio { get; set; } 

        

    }
}