using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OpenFlowCRMModels.Models
{
    public partial class Utenti
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserService
    {
        private readonly SQL_TESTContext _db;
        private readonly string _secretKey;

        public UserService(SQL_TESTContext db, IConfiguration configuration)
        {
            _db = db;
            string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (secret== null)
            {
                secret = "hbUx0XI3Fb1dNi+XMugJW/Oe1VKERBErXYc/HmngAhU="; //TODO:
                //throw new Exception("Set the JWT_SECRET environment variable"); 
            }
            _secretKey = secret;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Utenti.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "user"),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            using var hmac = new HMACSHA512(Convert.FromBase64String(_secretKey));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(Convert.FromBase64String(passwordHash));
        }
    }

}
