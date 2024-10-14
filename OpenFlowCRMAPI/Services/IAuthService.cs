using OpenFlowCRMModels.DTOs;

namespace OpenFlowCRMAPI.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(LogupDTO model, string role);
        Task<(int, string)> Login(LoginDTO model);
    }
}
