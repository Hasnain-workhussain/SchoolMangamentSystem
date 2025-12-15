using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMangamentSystem.Data;
using Microsoft.EntityFrameworkCore; 
using SchoolMangamentSystem.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMangamentSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDbContext _context;

        public CoursesController(SchoolDbContext context)
        {
            _context = context;
        }

        // ==========================
        // INDEX (LIST / SEARCH / SORT)
        // ==========================
        // GET: /Courses/Index
        public IActionResult Index(string searchString, string categoryFilter, string sortBy)
        {
            var courses = _context.Courses.AsQueryable();

            // 1. Search Logic
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString) || c.Description.Contains(searchString));
            }

            // 2. Filter Logic
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                courses = courses.Where(c => c.Category == categoryFilter);
            }

            // 3. Sorting Logic
            switch (sortBy)
            {
                case "name-desc":
                    courses = courses.OrderByDescending(c => c.CourseName);
                    break;
                case "price-asc":
                    courses = courses.OrderBy(c => c.Price);
                    break;
                case "price-desc":
                    courses = courses.OrderByDescending(c => c.Price);
                    break;
                default: // name-asc
                    courses = courses.OrderBy(c => c.CourseName);
                    break;
            }

            // Keep filters alive in the view
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCategory"] = categoryFilter;
            ViewData["CurrentSort"] = sortBy;

            return View(courses.ToList());
        }

        // ==========================
        // CREATE SECTION
        // ==========================
        // GET: /Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Courses/Create
        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle Image Upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        course.CourseImage = memoryStream.ToArray();
                    }
                }

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // ==========================
        // EDIT SECTION
        // ==========================
        // GET: /Courses/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: /Courses/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? imageFile)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Fetch existing course to preserve data (like image) if not changing
                    var existingCourse = await _context.Courses.FindAsync(id);
                    if (existingCourse == null) return NotFound();

                    // 2. Update fields
                    existingCourse.CourseName = course.CourseName;
                    existingCourse.Category = course.Category;
                    existingCourse.Price = course.Price;
                    existingCourse.Description = course.Description;

                    // 3. Handle Image Update (Only if a new file is uploaded)
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            existingCourse.CourseImage = memoryStream.ToArray();
                        }
                    }
                    // Note: If imageFile is null, we keep the existingCourse.CourseImage as is.

                    _context.Update(existingCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.Id == course.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // ==========================
        // DELETE SECTION
        // ==========================
        // POST: /Courses/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}