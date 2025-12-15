using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // <--- ADD THIS LINE

namespace SchoolMangamentSystem.ViewModels
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = "General";

        [Range(0, 10000)]
        [Column(TypeName = "decimal(18,2)")] // <--- ADD THIS LINE (Fixes the warning)
        public decimal Price { get; set; } = 0.00m;

        public byte[]? CourseImage { get; set; }
    }
}