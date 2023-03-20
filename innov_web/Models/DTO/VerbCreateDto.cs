using innov_web.Models.DTO;

namespace innov_web.Models.DTO
{
    public class VerbCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }

        public string Type { get; set; }
        public int GroupId { get; set; }
        public string QueryStatement { get; set; }

        public List<ParamtersDto> Paramters { get; set; } 
    }
}
