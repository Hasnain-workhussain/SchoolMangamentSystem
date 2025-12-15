using Microsoft.AspNetCore.Mvc;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels;
using System.Linq;

namespace SchoolMangamentSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // --- REPLACE THE OLD Index METHOD WITH THIS ONE ---
        // GET: /Students/Index
        public IActionResult Index(string searchString, string courseFilter)
        {
            // 1. Start with all students
            var students = _context.Students.AsQueryable();

            // 2. Apply Search (Name or Email)
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FullName.Contains(searchString) || s.Email.Contains(searchString));
            }

            // 3. Apply Course Filter
            if (!string.IsNullOrEmpty(courseFilter))
            {
                students = students.Where(s => s.CourseName == courseFilter);
            }

            // 4. Pass current filters back to View (so the inputs don't reset)
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCourse"] = courseFilter;

            return View(students.ToList());
        }
        // --------------------------------------------------

        // GET: /Students/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Students/Create
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        // ==========================
        // EDIT SECTION
        // ==========================

        // GET: /Students/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: /Students/Edit/5
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // ==========================
        // DELETE SECTION
        // ==========================

        // POST: /Students/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}