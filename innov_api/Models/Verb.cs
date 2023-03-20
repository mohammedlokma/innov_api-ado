using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innov_api.Models
{
    public class Verb
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public string TableName { get; set; }

        public string QueryStatement{ get; set; }
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Link { get; set; }
        public ICollection<Paramter> Paramters { get; set; }
    }
}
