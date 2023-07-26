using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFlowCRMModels.DTOs
{
    [Serializable]
    public class LoginDTO
    {
       public string username { get; set; }
        public string password { get; set; }
    }
}
