using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenFlowCRMAPI.Repository
{
    public class CustomJwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _key;
        public CustomJwtDataFormat(string key)
        {
            _key = key;
        }

        public string Protect(AuthenticationTicket data, string purpose = null)
        { return Protect(data); }

            public string Protect(AuthenticationTicket data)
        {
            // Serialize the authentication ticket to a JWT token string
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.CreateJwtSecurityToken(
                issuer: "MyCompany",
                audience: "MyCompany",
                subject: data.Principal.Identity as ClaimsIdentity,
                expires: data.Properties.ExpiresUtc?.DateTime.AddDays(5),
                issuedAt: data.Properties.IssuedUtc?.DateTime, 
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                    SecurityAlgorithms.HmacSha256));
            var tokenString = handler.WriteToken(jwtToken);

            return tokenString;
        }

        public AuthenticationTicket? Unprotect(string? protectedText, string? purpose)
        {
            return Unprotect(protectedText);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            // Validate the JWT token and create an authentication ticket from it
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                ValidateIssuer = true,
                ValidIssuer = "MyCompany",
                ValidateAudience = true,
                ValidAudience = "MyCompany",
                ValidateLifetime=false,
                ClockSkew = TimeSpan.Zero
            };
            var principal = handler.ValidateToken(protectedText, tokenValidationParameters, out var validatedToken);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), "OpenFlowCRMCookie");

            return ticket;
        }

        
    }

}
