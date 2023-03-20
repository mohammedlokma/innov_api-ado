using AutoMapper;
using innov_api.Data;
using innov_api.Models;
using innov_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Dynamic;
using System.Net;
using System.Security.Principal;
using task_api.Models;

namespace innov_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        protected ApiRespose _response;
        private ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GroupController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet("GetGroups")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiRespose>> GetGroups()
        {
            try
            {

                IEnumerable<Group> groups = await _dbContext.Groups.ToListAsync();
                var goupsDto = _mapper.Map<List<GroupDto>>(groups);
                _response.Result = goupsDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPost("CreateGroup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> CreateGroup([FromBody] GroupDto groupCreateDto)
        {
            try
            {
               
                if (await _dbContext.Groups.AsNoTracking().SingleOrDefaultAsync(i=>i.Name == groupCreateDto.Name) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Name already Exists!");
                    return BadRequest(ModelState);
                }

                if (groupCreateDto == null)
                {
                    return BadRequest();
                }

                Group group = _mapper.Map<Group>(groupCreateDto);

                await _dbContext.Groups.AddAsync(group);
                await _dbContext.SaveChangesAsync();
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetGroupVerbs")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetGroupVerbs(int groupId)
        {
            try
            {
                var group = await _dbContext.Groups.FirstOrDefaultAsync(i => i.Id == groupId); 
                IEnumerable<Verb> verbs = await _dbContext.Verbs.Where(i => i.GroupId == groupId).ToListAsync();
                
               var verbsDto = _mapper.Map<List<VerbDto>>(verbs);
                var groupVerbsDto = new GroupVerbsDto()
                {
                    GroupName = group.Name,
                    VerbsDto = verbsDto

                };
                _response.Result = groupVerbsDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
       

    }
}
