using System.ComponentModel.DataAnnotations;

namespace SchoolMangamentSystem.ViewModels
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Administrator";

        
            // ... existing fields ...

            // Add this new field
            public byte[]? ProfilePicture { get; set; }
        }


    }
