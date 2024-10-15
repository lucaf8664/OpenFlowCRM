using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.DTOs;

namespace OpenFlowCRMAPI.Services
{
    public interface IAuthService
    {
        //Task<(int, string)> Registration(LogupDTO model, string role);
        Task<string> Login(LoginDTO model);
    }
}
