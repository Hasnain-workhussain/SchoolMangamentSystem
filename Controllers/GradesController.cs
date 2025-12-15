using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMangamentSystem.Controllers
{
    public class GradesController : Controller
    {
        private readonly SchoolDbContext _context;

        public GradesController(SchoolDbContext context)
        {
            _context = context;
        }

        // ==========================
        // INDEX (List View)
        // ==========================
        public async Task<IActionResult> Index()
        {
            var grades = await _context.Grades.ToListAsync();
            // Create a lookup dictionary: ID -> Name
            ViewBag.StudentNames = await _context.Students.ToDictionaryAsync(s => s.Id, s => s.FullName);
            return View(grades);
        }

        // ==========================
        // CREATE (GET)
        // ==========================
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade grade)
        {
            // FIX: Forcefully remove ANY validation error containing the word "Student"
            // This covers "Student", "Students", "StudentNavigation", etc.
            foreach (var key in ModelState.Keys.Where(k => k.Contains("Student") && !k.EndsWith("Id")).ToList())
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                if (grade.GPA == 0)
                {
                    grade.GPA = Math.Round((grade.MidTerm + grade.Quiz + grade.FinalTerm) / 3.0 / 20.0, 2);
                }

                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // DEBUG: If it fails again, the Red Box will tell us the new reason
            var errorList = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            ViewBag.DebugErrors = string.Join(" | ", errorList);

            PopulateDropdowns(grade.StudentId);
            return View(grade);
        }

        // ==========================
        // EDIT (GET)
        // ==========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            PopulateDropdowns(grade.StudentId);
            return View(grade);
        }

        // ==========================
        // EDIT (POST)
        // ==========================
        // ==========================
        // EDIT (POST) - FIXED
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grade grade)
        {
            if (id != grade.Id) return NotFound();

            // FIX 1: Forcefully remove ANY validation error related to the Student object
            // This prevents "The Student field is required" error
            foreach (var key in ModelState.Keys.Where(k => k.Contains("Student") && !k.EndsWith("Id")).ToList())
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalculate GPA just to be safe
                    grade.GPA = Math.Round((grade.MidTerm + grade.Quiz + grade.FinalTerm) / 3.0 / 20.0, 2);

                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Grades.Any(e => e.Id == grade.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // FIX 2: Capture any other errors so we can see them
            var errorList = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            ViewBag.DebugErrors = string.Join(" | ", errorList);

            PopulateDropdowns(grade.StudentId);
            return View(grade);
        }

        // ==========================
        // DELETE
        // ==========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // HELPER
        // ==========================
        private void PopulateDropdowns(object selectedStudent = null)
        {
            // KEY FIX: Use "StudentList" to avoid conflict with "StudentId"
            ViewData["StudentList"] = new SelectList(_context.Students, "Id", "FullName", selectedStudent);

            if (_context.Courses.Any())
            {
                ViewBag.Courses = new SelectList(_context.Courses, "CourseName", "CourseName");
            }
            else
            {
                ViewBag.Courses = new SelectList(new[] { "Biology", "Mathematics", "Computer Science", "Physics", "Chemistry" });
            }
        }
    }
}