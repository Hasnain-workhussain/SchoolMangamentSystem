using System.Collections.Generic;

namespace SchoolMangamentSystem.ViewModels
{
    public class ResultViewModel
    {
        public Student? Student { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();

        // Summary Stats
        public double TotalGPA { get; set; }
        public double TotalMarks { get; set; }
        public double ObtainedMarks { get; set; }
        public double Percentage { get; set; }
    }
}