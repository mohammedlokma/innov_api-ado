using innov_web.Models.DTO;
using innov_web.Services;
using innov_web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace innov_web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<IActionResult> Index()
        {
            List<GroupDto> list = new();

            var response = await _groupService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GroupDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> GroupCreate()
        {
            GroupCreateDto groupDto = new();
            return View(groupDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupCreate(GroupDto model)
        {
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
              
                model.ConnectionString = (model.ConnectionString).Replace(@"\\", @"\");
                var response = await _groupService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Group created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        public async Task<IActionResult> GroupVerbs(int groupId)
        {

            var groupVerbs = new GroupVerbsDto();
            var response = await _groupService.GetGroupVerbsAsync<APIResponse>(groupId);
            if (response != null && response.IsSuccess)
            {
                 groupVerbs = JsonConvert.DeserializeObject<GroupVerbsDto>(Convert.ToString(response.Result));
            }
            groupVerbs.Id = groupId;
            return View(groupVerbs);
        }

    }
}
