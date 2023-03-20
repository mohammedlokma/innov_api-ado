using AutoMapper;
using innov_api.Data;
using innov_api.Models;
using innov_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
using System.Net;
using task_api.Models;

namespace innov_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerbController : ControllerBase
    {
        protected ApiRespose _response;
        private ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public VerbController(ApplicationDbContext dbContext, IMapper mapper,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _response = new();
        }
       
        [HttpPost("CreateVerb")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> CreateVerb( [FromBody] VerbCreateDto verbCreateDto)
        {
            try
            {
                var requestUrl = $"{Request.Scheme}://{Request.Host.Value}";

                if (await _dbContext.Verbs.AsNoTracking().SingleOrDefaultAsync(i => i.Name == verbCreateDto.Name) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Name already Exists!");
                    return BadRequest(ModelState);
                }

                if (verbCreateDto == null)
                {
                    return BadRequest();
                }
                var transaction = _dbContext.Database.BeginTransaction();
                try
                {
                    Verb verb = _mapper.Map<Verb>(verbCreateDto);
                    var group = await _dbContext.Groups.AsNoTracking().FirstOrDefaultAsync(i => i.Id == verb.GroupId); 
                    verb.Link = "/BackEnd/" + group.Name+"/"+ verb.TableName+"/"+verb.Name;
                    await _dbContext.Verbs.AddAsync(verb);
                    await _dbContext.SaveChangesAsync();

                    foreach (var param in verbCreateDto.Paramters)
                    {
                        param.VerbId = verb.Id;
                    }

                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.Result = requestUrl + verb.Link;
                    return Ok(_response);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();


                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetVerb")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetGroupVerbs(int verbId)
        {
            try
            {
                var verb = await _dbContext.Verbs.FirstOrDefaultAsync(i => i.Id == verbId);

                var verbDto = _mapper.Map<VerbDto>(verb);
               
                _response.Result = verbDto;
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
        [HttpGet("GetTables")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetTables(int groupId)
        {
            try
            {
                var group = await _dbContext.Groups.FirstOrDefaultAsync(i => i.Id == groupId);

                var connectionString = group.ConnectionString;
                var query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME OFFSET 1 ROWS";
                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                da.Fill(dt);
                var data = dt.AsEnumerable().Select(i => i.ItemArray).ToList();
                var list = new List<object>();


                IDictionary<string, object> myDict = new Dictionary<string, object>();
                var eo = new ExpandoObject();
                var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
                for (int j = 0; j <= data.Count -1 ; j++)
                {
                    myDict.Add("Name", data[j][0].ToString());

                    foreach (var kvp in myDict)
                    {
                        eoColl.Add(kvp);
                    }
                    list.Add(eoColl);
                    myDict = new Dictionary<string, object>();
                    eo = new ExpandoObject();
                    eoColl = (ICollection<KeyValuePair<string, object>>)eo;
                }


                connection.Close();
                _response.Result = list;
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
