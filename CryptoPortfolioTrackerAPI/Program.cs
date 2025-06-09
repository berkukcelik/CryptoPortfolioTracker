using System.Text;
using CryptoPortfolioTrackerAPI.Data;
using CryptoPortolioTrackerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {

            options.TokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), 
            ValidateIssuer = true, 
            ValidIssuer = builder.Configuration["Jwt:Issuer"], 
            ValidateAudience = true, 
            ValidAudience = builder.Configuration["Jwt:Audience"], 
            ValidateLifetime = true, 
            ClockSkew = TimeSpan.Zero 
            };

        });

builder.Services.AddControllers();

builder.Services.AddScoped<ITokenGenerateService, TokenGenerateService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
