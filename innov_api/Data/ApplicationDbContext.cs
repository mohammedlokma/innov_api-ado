using innov_api.Models;
using Microsoft.EntityFrameworkCore;

namespace innov_api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Verb> Verbs { get; set; }
        public DbSet<Paramter> Paramters { get; set; }
    }
}
