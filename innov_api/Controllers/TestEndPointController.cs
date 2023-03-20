using innov_api.Models.DTOs;
using innov_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using task_api.Models;
using AutoMapper;
using innov_api.Data;
using System.Data;
using System.Dynamic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace innov_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEndPointController : ControllerBase
    {
        protected ApiRespose _response;
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;
        public TestEndPointController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new();
            _mapper = mapper;
        }
        [HttpGet("Select")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> Select([FromQuery] DbConfigDto dbConfig)
        {
            try
            {


                if (dbConfig == null)
                {
                    return BadRequest();
                }
                var convertedObj = new Dictionary<string, dynamic>();
                if (dbConfig.paramters != "[]")
                {
                     convertedObj = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(dbConfig.paramters);
                }
                var connectionString = dbConfig.ConnectionString;

                var query = dbConfig.QueryStatement;

                var columnsQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + dbConfig.TableName + "'";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(query, connection);
                if (convertedObj.Keys.Count > 0)
                {
                    foreach (var param in convertedObj.Keys)
                    {
                        var key = param;
                        var value = convertedObj[param];
                        cmd.Parameters.AddWithValue('@' + param, convertedObj[param]);

                    }
                }
                SqlCommand cmd2 = new SqlCommand(columnsQuery, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();
                connection.Open();
                da.Fill(dt);
                var columns = dt.AsEnumerable().Select(i => i.ItemArray).ToList();

                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();

                da2.Fill(dt2);
                var data = dt2.AsEnumerable().Select(i => i.ItemArray).ToList();
                var list = new List<object>();


                IDictionary<string, object> myDict = new Dictionary<string, object>();
                var eo = new ExpandoObject();
                var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
                for (int j = 0; j <= data.Count - 1; j++)
                {
                    for (int i = 0; i <= columns.Count - 1; i++)
                    {
                        myDict.Add(columns[i][0].ToString(), data[j][i].ToString());

                    };
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
        [HttpGet("Execute")]
        [HttpPost("Execute")]
        [HttpDelete("Execute")]
        [HttpPut("Execute")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> Execute([FromQuery] DbConfigDto dbConfig)
        {
            try
            {

                if (dbConfig == null)
                {
                    return BadRequest();
                }
                var convertedObj = new Dictionary<string, dynamic>();
                if (dbConfig.paramters != "[]")
                {
                    convertedObj = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(dbConfig.paramters);
                }
                var connectionString = dbConfig.ConnectionString;

                var query = dbConfig.QueryStatement;


                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(query, connection);
                if (convertedObj.Keys.Count > 0)
                {
                    foreach (var param in convertedObj.Keys)
                    {
                        var key = param;
                        var value = convertedObj[param];
                        cmd.Parameters.AddWithValue('@' + param, convertedObj[param]);

                    }
                }
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                // await _db.SaveChangesAsync();
                _response.Result =  dbConfig.MethodType + " Successfuly";
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response.Result);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> GetAll()
        {
           
            return "Get All From DB";

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> CreateNew()
        {
            try
            {

               
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = "Inserted Successfully";
                return Ok(_response.Result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> Update()
        {
            try
            {


                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = "Updated Successfully";
                return Ok(_response.Result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetByParams")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetByParams([FromQuery] params int[] par)
        {
          
            return "Get With params";

        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteByID( int id)
        {

            return "delete item"+ id +"from db";

        }
        [HttpGet("GetParams")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiRespose>> GetParams(int verbId)
        {
            var data = await _dbContext.Paramters.Where(i => i.VerbId == verbId).ToListAsync();
            var dataDto = _mapper.Map<List<ParamtersDto>>(data);
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = dataDto;
            return Ok(_response);
            

        }
    }
}
