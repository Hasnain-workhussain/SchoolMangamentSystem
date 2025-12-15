using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMangamentSystem.Controllers
{
    public class ResultsController : Controller
    {
        private readonly SchoolDbContext _context;

        public ResultsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: /Results/Index
        public async Task<IActionResult> Index(string searchEmail)
        {
            var model = new ResultViewModel();

            if (!string.IsNullOrEmpty(searchEmail))
            {
                // 1. Find the student first
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == searchEmail || s.FullName == searchEmail);

                if (student != null)
                {
                    model.Student = student;

                    // FIX: Query Grades using StudentId (The reliable Foreign Key)
                    // Old way: .Where(g => g.StudentName == student.FullName) -> Unreliable
                    model.Grades = await _context.Grades
                        .Where(g => g.StudentId == student.Id)
                        .ToListAsync();

                    if (model.Grades.Any())
                    {
                        // FIX: Treat each course as having a max of 100%
                        model.TotalMarks = model.Grades.Count * 100;

                        // Calculate obtained marks (Logic: Sum of average per subject)
                        model.ObtainedMarks = model.Grades.Sum(g => (g.MidTerm + g.Quiz + g.FinalTerm) / 3.0);

                        // Calculate Final Percentage
                        model.Percentage = Math.Round((model.ObtainedMarks / model.TotalMarks) * 100, 2);

                        // Average GPA
                        model.TotalGPA = Math.Round(model.Grades.Average(g => g.GPA), 2);
                    }
                }
                else
                {
                    ViewBag.Message = "Student not found.";
                }
            }
            return View(model);
        }
    }
}