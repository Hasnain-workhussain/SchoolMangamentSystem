using System.ComponentModel.DataAnnotations;

namespace SchoolMangamentSystem.ViewModels
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty; // Initialized to avoid warning

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Initialized

        [Required]
        public string Password { get; set; } = string.Empty; // Initialized

        public string PhoneNumber { get; set; } = string.Empty; // Initialized

        public string CourseName { get; set; } = string.Empty; // Initialized

        public string Status { get; set; } = "Active"; // Set a useful default value
    }
}