using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoProjectMVC.Models;

namespace DemoProjectMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Students.Include(s => s.City).Include(s => s.Country).Include(s => s.State);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Students students, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                if (pictureFile != null && pictureFile.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/students", pictureFile.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        pictureFile.CopyTo(stream);
                    }
                    students.Picture = $"{pictureFile.FileName}";

                }
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName", students.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", students.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", students.StateId);
            return View(students);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName", students.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", students.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", students.StateId);
            return View(students);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Students students, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var student = await _context.Students.FindAsync(students.Id);

                    if (pictureFile != null && pictureFile.Length > 0)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                         pictureFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            pictureFile.CopyTo(stream);
                        }
                        students.Picture = $"{pictureFile.FileName}";
                    }
                    else
                    {
                        students.Picture = student.Picture;
                    }

                    student.Picture = students.Picture;
                    student.FirstName = students.FirstName;
                    student.LastName = students.LastName;
                    student.EmailAddress = students.EmailAddress;
                    student.Gender = students.Gender;
                    student.Ssc = students.Ssc;
                    student.Hsc = students.Hsc;
                    student.Bsc = students.Bsc;
                    student.Msc = students.Msc;
                    student.CountryId = students.CountryId;
                    student.StateId = students.StateId;
                    student.CityId = students.CityId;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName", students.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", students.CountryId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", students.StateId);
            return View(students);

        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'AppDbContext.Students'  is null.");
            }
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {
                _context.Students.Remove(students);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
