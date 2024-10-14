using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.Models;
using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using OpenFlowCRMModels.DTOs;
using System.Security.Cryptography;

namespace OpenFlowCRMAPI.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _iconfiguration;
        private readonly SQL_TESTContext _context;
        public JWTManagerRepository(IConfiguration iconfiguration, SQL_TESTContext context)
        {
            _context = context;
            this._iconfiguration = iconfiguration;
        }
        public Tokens Authenticate(LoginDTO loginDTO)
        {
            var hashAlgorithm = new HMACBlake2B(512);
            hashAlgorithm.Initialize();
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));
            var hashString = Convert.ToBase64String(hash);

            if (!_context.Utenti.Any(x => x.Username == loginDTO.username && x.PasswordHash == hashString))
            {
                return null;
            }

            string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");

            if (secret== null)
            {
                secret = "hbUx0XI3Fb1dNi+XMugJW/Oe1VKERBErXYc/HmngAhU=";

            }

            // Generate tokens
            var accessToken = GenerateAccessToken(loginDTO, secret);

            var response = new Tokens
            {
                AccessToken = accessToken
            };

            return response;
        }

        ////////////////
        ///

        public string GenerateAccessToken(LoginDTO user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var now = DateTime.UtcNow.AddMinutes(15);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.username.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
        {
            // Implement logic to generate a new access token from the refresh token
            // Verify the refresh token and extract necessary information (e.g., user ID)
            // Then generate a new access token

            // For demonstration purposes, return a new token with an extended expiry
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(15), // Extend expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
