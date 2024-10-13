using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.DTOs;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMAPI.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(LoginDTO users);
    }

}
