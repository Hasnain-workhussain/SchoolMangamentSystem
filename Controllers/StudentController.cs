using Microsoft.AspNetCore.Mvc;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace SchoolMangamentSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // LOGIN SECTION
        // ==========================================

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, go to Dashboard
            if (HttpContext.Session.GetInt32("StudentId") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // This checks the database. 
            // Since we removed Register, this data MUST have come from the Admin Portal.
            var student = _context.Students
                .FirstOrDefault(s => s.Email == email && s.Password == password);

            if (student != null)
            {
                HttpContext.Session.SetInt32("StudentId", student.Id);
                HttpContext.Session.SetString("StudentName", student.FullName);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // ==========================================
        // DASHBOARD (INDEX) SECTION
        // ==========================================

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null) return RedirectToAction("Login");

            var student = _context.Students.Find(studentId);

            // 1. Fetch exams
            var upcomingExams = _context.Exams
                .Where(e => e.CourseName == student.CourseName && e.ExamDate >= DateTime.Today)
                .OrderBy(e => e.ExamDate)
                .ToList();

            // 2. Fetch Recent Grades
            // FIX: Changed 'g.Id' to 'g.StudentId'
            var recentGrades = _context.Grades
                .Where(g => g.StudentId == studentId)
                .OrderByDescending(g => g.Id)
                .ToList();

            // 3. Calculate GPA
            double overallGPA = 0.00;
            if (recentGrades.Any())
            {
                overallGPA = recentGrades.Average(g => g.GPA);
            }

            ViewBag.UpcomingExams = upcomingExams;
            ViewBag.RecentGrades = recentGrades;
            ViewBag.OverallGPA = Math.Round(overallGPA, 2);

            return View("Dashboard", student);
        }

        public IActionResult Dashboard()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}