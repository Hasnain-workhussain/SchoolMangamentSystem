using System.Collections.Generic;

namespace SchoolMangamentSystem.ViewModels
{
    public class AdminDashboardViewModel
    {
        // --- 1. Admin Profile Info ---
        public string AdminName { get; set; } = string.Empty;
        public string AdminRole { get; set; } = string.Empty;
        public string AdminInitials { get; set; } = string.Empty;

        // --- 2. Stats Cards ---
        public int TotalStudents { get; set; }
        public int ActiveCourses { get; set; }
        public int NewEnrollments { get; set; }
        public int TotalTeachers { get; set; }

        // --- 3. Chart Data (Required for Graphs) ---
        public List<string> AttendanceLabels { get; set; } = new();
        public List<int> AttendanceData { get; set; } = new();

        public List<string> GradeLabels { get; set; } = new();
        public List<int> GradeData { get; set; } = new();
    }
}