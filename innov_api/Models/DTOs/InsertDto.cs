namespace innov_api.Models.DTOs
{
    public class InsertDto
    {
        public Dictionary<string, string> jsonObject { get; set; }
        public DbConfigDto dbConfig { get; set; }
    }
}
