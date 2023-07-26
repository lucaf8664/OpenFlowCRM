using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMAPI.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Utenti users);
    }

}
