using innov_api.Models.DTOs;

namespace innov_api.Models.DTOs
{
    public class VerbCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }

        public string Type { get; set; }
        public int GroupId { get; set; }
        public string QueryStatement { get; set; }
        public ICollection<ParamtersDto> Paramters { get; set; }
    }
}
