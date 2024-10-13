using OpenFlowCRMAPI.Models;
using OpenFlowCRMAPI.Repository;
using OpenFlowCRMModels.DTOs;
using OpenFlowCRMModels.Models;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace OpenFlowCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly SQL_TESTContext _context;
        protected readonly IConfiguration _config;

        public UtentiController(IJWTManagerRepository jWTManager, IConfiguration config, SQL_TESTContext context)
        {
            _context = context;
            _config = config;
            this._jWTManager = jWTManager;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        private async Task<ActionResult> AddUser(LoginDTO usersdata)
        {
            var hashAlgorithm = new HMACBlake2B(512);
            hashAlgorithm.Initialize();

            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(usersdata.username));

            var utenti = new Utenti();

            utenti.Username = usersdata.password;
            utenti.PasswordHash = Convert.ToBase64String(hash);

            // Add the user to the Users table in the database
            using (var db = new SQL_TESTContext())
            {
                await db.Utenti.AddAsync(utenti);
                db.SaveChanges();
            }
            return Ok();
        } 

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<Tokens>> Authenticate(IJWTManagerRepository jwtManager, LoginDTO usersdata)
        {
            try
            {
                Tokens response = jwtManager.Authenticate(usersdata);

                if (response == null) return Unauthorized();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }


    public interface IJwtTokenGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _key;

        public JwtTokenGenerator(string key)
        {
            _key = key;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyCompany",
                audience: "MyCompany",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
