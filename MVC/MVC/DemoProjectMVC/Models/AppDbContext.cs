using Microsoft.EntityFrameworkCore;
using DemoProjectMVC.Models;

namespace DemoProjectMVC.Models
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Students> Students { get; set; }


    }
}
