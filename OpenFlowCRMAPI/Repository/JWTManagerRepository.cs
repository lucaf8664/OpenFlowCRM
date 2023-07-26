using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.Models;
using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace OpenFlowCRMAPI.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
    {
        { "user1","password1"},
        { "user2","password2"},
        { "user3","password3"},
    };

        private readonly IConfiguration _iconfiguration;
        private readonly SQL_TESTContext _context;
        public JWTManagerRepository(IConfiguration iconfiguration, SQL_TESTContext context)
        {
            _context = context;
            this._iconfiguration = iconfiguration;
        }
        public Tokens Authenticate(Utenti users)
        {
            string ? secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (secret== null)
            {
                secret = "hbUx0XI3Fb1dNi+XMugJW/Oe1VKERBErXYc/HmngAhU="; //TODO:
                //throw new Exception("Set the JWT_SECRET environment variable"); 
            }
            var hashAlgorithm = new HMACBlake2B(512);
            hashAlgorithm.Initialize();
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(users.PasswordHash));
            var hashString = Convert.ToBase64String(hash);

            if (!_context.Utenti.Any(x => x.Username == users.Username && x.PasswordHash == hashString))
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, users.Username)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };



        }
    }
}
