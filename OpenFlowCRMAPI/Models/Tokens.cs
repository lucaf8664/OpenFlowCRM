using Microsoft.IdentityModel.Tokens;
using OpenFlowCRMModels.DTOs;
using OpenFlowCRMModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OpenFlowCRMAPI.Models
{
    public class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

}
