using Microsoft.EntityFrameworkCore;

namespace AntilopaApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Owner> Owners {get; set;}
        public DbSet<Car> Cars { get; set; }
    }
}