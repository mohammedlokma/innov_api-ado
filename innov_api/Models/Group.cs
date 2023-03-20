using System.ComponentModel.DataAnnotations;

namespace innov_api.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string DbType { get; set; }
        public string ConnectionString { get; set; }
        public ICollection<Verb> Verbs { get; set; }
    }
}
