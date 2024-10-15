using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.DTOs;
using OpenFlowCRMModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenFlowCRMAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly SQL_TESTContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(SQL_TESTContext context, IConfiguration configuration)
        {
            this._context = context;
            _configuration = configuration;

        }
        //public async Task<(int, string)> Registration(LogupDTO model, string role)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return (0, "User already exists");

        //    Utenti user = new()
        //    {
        //        Username = model.Username
        //    };
        //    var createUserResult = await userManager.CreateAsync(user, model.Password);
        //    if (!createUserResult.Succeeded)
        //        return (0, "User creation failed! Please check user details and try again.");

        //    if (!await roleManager.RoleExistsAsync(role))
        //        await roleManager.CreateAsync(new IdentityRole(role));

        //    if (await roleManager.RoleExistsAsync(role))
        //        await userManager.AddToRoleAsync(user, role);

        //    return (1, "User created successfully!");
        //}

        public async Task<string> Login(LoginDTO loginDTO)
        {

            var user = Authenticate(loginDTO);

            if (user is not null)
            {
                var token = GenerateToken(user);
                return token;
            }

            return "user not found";



        }

        private Utenti Authenticate(LoginDTO loginDTO)
        { 

            var hashAlgorithm = new HMACBlake2B(512);
            hashAlgorithm.Initialize();
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            var hashString = Convert.ToBase64String(hash);

            var currentUser = _context.Utenti.FirstOrDefault(x => x.Username == loginDTO.Username && x.PasswordHash == hashString);

            return currentUser;
            
        }


        // To generate token
        private string GenerateToken(Utenti user)
        {

            var key = Environment.GetEnvironmentVariable("JWT_SECRET");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
            };
            var token = new JwtSecurityToken(_configuration["Jwt:ValidIssuer"],
                _configuration["Jwt:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return tokenString;

        }
    }
}
