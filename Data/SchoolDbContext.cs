using Microsoft.EntityFrameworkCore;
using SchoolMangamentSystem.ViewModels; 

namespace SchoolMangamentSystem.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Grade> Grades { get; set; }

    }
}