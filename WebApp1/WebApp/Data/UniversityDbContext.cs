using Microsoft.EntityFrameworkCore;
using WebApp.Models.Domain;

namespace WebApp.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
