namespace innov_api.Models.DTOs
{
    public class DbConfigDto
    {
        public string ConnectionString{ get; set; }
        public string QueryStatement { get; set; }
        
        public string MethodType { get; set; }
        public string TableName { get; set; }
        public string paramters { get; set; }


    }
}
