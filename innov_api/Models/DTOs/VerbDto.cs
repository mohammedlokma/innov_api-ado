using System.ComponentModel.DataAnnotations;

namespace innov_api.Models.DTOs
{
    public class VerbDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public string Type { get; set; }

        public string QueryStatement { get; set; }
        public string Link { get; set; }
        public int GroupId { get; set; }
    }
}
