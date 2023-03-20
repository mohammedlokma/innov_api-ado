using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using innov_web.Models.DTO;

namespace innov_web.Models.VM
{
    public class TestVM
    {
        public TestVM()
        {
            Verb = new VerbDto();
        }
        public VerbDto Verb { get; set; }
      
        public IEnumerable<ParamtersDto> Paramters{ get; set; }
    }
}
