using innov_web.Models.DTO;
using innov_web.Models.VM;
using innov_web.Services;
using innov_web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using innov_web.Models.DTO;

namespace innov_web.Controllers
{
    public class VerbController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IVerbService _verbService;
        public VerbController(IGroupService groupService, IVerbService verpService)
        {
            _groupService = groupService;
            _verbService = verpService;
        }
        public async Task<IActionResult> VerbCreate(int groupId)
        {
            CreateVerbVM createVerbVm = new();
            createVerbVm.Verb.GroupId = groupId;
            var response = await _groupService.GetAllAsync<APIResponse>();
            var tableRepsonse = await _verbService.GetTablesNamesAsync<APIResponse>(groupId);

            if (response != null && response.IsSuccess && tableRepsonse != null && tableRepsonse.IsSuccess)
            {
                createVerbVm.GroupList = JsonConvert.DeserializeObject<List<GroupDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                createVerbVm.TableList = JsonConvert.DeserializeObject<List<TableDto>>
                     (Convert.ToString(tableRepsonse.Result)).Select(i => new SelectListItem
                     {
                         Text = i.Name,
                     });
            }
            return View(createVerbVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerbCreate( CreateVerbVM model)
        {
            model.Verb.Paramters = new List<ParamtersDto>();
            //if (!model.CheckParams)
            //{
            //    model.Verb.Paramters.Clear();
            //}
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
           
                
                
                var response = await _verbService.CreateAsync<APIResponse>(model.Verb);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Verb created successfully";
                    return RedirectToAction("GroupVerbs", "Group", new { groupId = model.Verb.GroupId });
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            
            return RedirectToAction("GroupVerbs", "Group", new { groupId = model.Verb.GroupId });
        }
        public async Task<IActionResult> TestEndPoint(int verbId)
        {

            List<ParamtersDto> list = new();

            var response = await _verbService.GetParamtersAsync<APIResponse>(verbId);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ParamtersDto>>(Convert.ToString(response.Result));
            }
            var verbResponse = await _verbService.GetVerbAsync<APIResponse>(verbId);
            VerbDto verb = new VerbDto();
            if (verbResponse != null && verbResponse.IsSuccess)
            {
                 verb = JsonConvert.DeserializeObject<VerbDto>(Convert.ToString(verbResponse.Result));
            }
            TestVM testVm = new TestVM()
            {
                Paramters = list,
                Verb = verb
            };
            return View(testVm);
        }
        

    }
}
