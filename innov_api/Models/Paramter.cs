using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innov_api.Models
{
    public class Paramter
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        [ForeignKey("VerbId")]
        public int VerbId { get; set; }
        public Verb Verb { get; set; }
    }
}
