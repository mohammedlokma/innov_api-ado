using innov_web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace innov_web.Models.VM
{
    public class CreateVerbVM
    {
        public CreateVerbVM()
        {
            Verb = new VerbCreateDto();
        }
        public VerbCreateDto Verb { get; set; }
        //public int numOfParamters { get; set; }
        //[DisplayName("There are params")]
        //[BindProperty]
        //public bool CheckParams { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GroupList { get; set; }
        public IEnumerable<SelectListItem> TableList { get; set; }
    }
}
