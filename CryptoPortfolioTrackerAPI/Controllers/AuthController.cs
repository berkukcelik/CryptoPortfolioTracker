using CryptoPortfolioTrackerAPI.Data;
using CryptoPortfolioTrackerAPI.Models;
using CryptoPortfolioTrackerAPI.Utilities;
using CryptoPortolioTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        private readonly ITokenGenerateService _tokenservice;
        public AuthController(ApplicationDbContext context, ITokenGenerateService tokenservice)
        {

            _context = context;
            _tokenservice = tokenservice;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userDto.Username == null || userDto.Email == null || userDto.Password == null)
            {
                return BadRequest("Null olamaz");
            }
            var user = new User()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                HashedPassword = PasswordHasher.HashPassword(userDto.Password)
            };
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok("Kullanici olusturuldu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata olustu {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
                if (user == null || !PasswordHasher.VerifyPassword(userDto.Password, user.HashedPassword))
                {
                    return Unauthorized("Kullanici adi ya da sifre yanlis!");
                }
                
                var token = _tokenservice.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Bir hata olustu {ex.Message}");
                }
            
            
        }
    }
}