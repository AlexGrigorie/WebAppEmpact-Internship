using Microsoft.EntityFrameworkCore;
using WebAppEmpact.Data.DbModels;

namespace WebAppEmpact.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<NewsDbModel> News { get; set; }
    }
}
