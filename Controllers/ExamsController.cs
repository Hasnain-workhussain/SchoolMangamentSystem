using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMangamentSystem.Controllers
{
    public class ExamsController : Controller
    {
        private readonly SchoolDbContext _context;

        public ExamsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: /Exams/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exams.ToListAsync());
        }

        // ==========================
        // CREATE
        // ==========================
        public IActionResult Create()
        {
            PopulateCourseList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCourseList();
            return View(exam);
        }

        // ==========================
        // EDIT (UPDATE)
        // ==========================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null) return NotFound();

            PopulateCourseList(); // Load courses for the dropdown
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exam exam)
        {
            if (id != exam.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Exams.Any(e => e.Id == exam.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateCourseList();
            return View(exam);
        }

        // ==========================
        // DELETE
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper to load course dropdown
        private void PopulateCourseList()
        {
            ViewBag.CourseList = _context.Courses.Select(c => c.CourseName).Distinct().ToList();
        }
    }
}