using Microsoft.AspNetCore.Mvc;
using SchoolMangamentSystem.ViewModels;

namespace SchoolMangamentSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                AdminName = "Davey Jones",
                AdminRole = "Administrator",
                AdminInitials = "DJ",

                // Stats for the cards
                TotalStudents = 1254,
                ActiveCourses = 24,
                NewEnrollments = 98,
                TotalTeachers = 42,

                // Chart 1: Attendance Trends (Line)
                AttendanceLabels = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug" },
                AttendanceData = new List<int> { 85, 88, 90, 92, 91, 93, 94, 92 },

                // Chart 2: Grades Trends (Bar)
                GradeLabels = new List<string> { "Math", "Physics", "Chem", "English", "History" },
                GradeData = new List<int> { 86, 79, 92, 88, 81 }
            };

            return View(model);
        }
    }
}