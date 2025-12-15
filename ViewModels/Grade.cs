using System.ComponentModel.DataAnnotations;

namespace SchoolMangamentSystem.ViewModels
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Student")]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Course")]
        public string CourseName { get; set; } = string.Empty;

        [Range(0, 100)]
        public double MidTerm { get; set; }

        [Range(0, 100)]
        public double Quiz { get; set; }

        [Range(0, 100)]
        public double FinalTerm { get; set; }

        public double GPA { get; set; } // We will calculate this automatically

        public string GetLetterGrade()
        {
            if (FinalTerm >= 90) return "A+";
            if (FinalTerm >= 85) return "A";
            if (FinalTerm >= 80) return "B+";
            if (FinalTerm >= 75) return "B";
            if (FinalTerm >= 70) return "C+";
            if (FinalTerm >= 60) return "C";
            return "F";
        }
    }
}
