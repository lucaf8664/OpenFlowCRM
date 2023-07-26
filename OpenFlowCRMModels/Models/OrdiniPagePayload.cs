using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenFlowCRMModels.Models
{
    [Serializable]
    public class ModelliClientiPair
    {
        public List<string> CatalogoModelli { get; set; }
        public List<SelectListItem> Clienti { get;set; }
    }
}
