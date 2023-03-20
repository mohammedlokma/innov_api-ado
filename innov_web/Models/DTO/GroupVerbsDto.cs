namespace innov_web.Models.DTO
{
    public class GroupVerbsDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<VerbDto> verbsDto { get; set; }
    }
}
