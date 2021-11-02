using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.EF.Contexts
{
    public class Context : IdentityDbContext<User>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; } = default;
        public DbSet<Teacher> Teachers { get; set; } = default;
        public DbSet<Course> Courses { get; set; } = default;
        public DbSet<Specialization> Specializations { get; set; } = default;
        public DbSet<StudentRequest> StudentRequests { get; set; } = default;
        public DbSet<Manager> Managers { get; set; }

        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
