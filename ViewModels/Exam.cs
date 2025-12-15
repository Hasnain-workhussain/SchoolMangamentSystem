using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolMangamentSystem.ViewModels
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Exam Name")]
        public string ExamName { get; set; } = string.Empty; // e.g. "Midterm"

        [Required]
        [Display(Name = "Exam Date")]
        public DateTime ExamDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Assigned Course")]
        public string CourseName { get; set; } = string.Empty; // e.g. "Mathematics"
    }
}