namespace CryptoPortfolioTrackerAPI.Models
{
    public class User
    {

        // nullable property uyarısı için ? -> null olabileceğini belirtmek için 
        // = null! null olmaycağını garanti etmek için 
        // constructor ile default değer de atayabiliriz
        public int Id { get; set; }
        public string Username { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public string Email { get; set; } = null!;
        
        public List<UserPortfolio> UserPortfolios { get; set; } = new List<UserPortfolio>();
        
    }
}