﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoProjectMVC.Models;

namespace DemoProjectMVC.Controllers
{
    public class StateController : Controller
    {
        private readonly AppDbContext _context;

        public StateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: State
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.States.Include(s => s.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: State/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: State/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            return View();
        }

        // POST: State/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StateName,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", state.CountryId);
            return View(state);
        }

        // GET: State/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", state.CountryId);
            return View(state);
        }

        // POST: State/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StateName,CountryId")] State state)
        {
            if (id != state.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", state.CountryId);
            return View(state);
        }

        // GET: State/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'AppDbContext.States'  is null.");
            }
            var state = await _context.States.FindAsync(id);
            if (state != null)
            {
                _context.States.Remove(state);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
          return _context.States.Any(e => e.Id == id);
        }

        public JsonResult GetCitiesByStateId(int stateId)
        {
            List<City> cities = new List<City>();
            cities = (from city in _context.City
                      where city.StateId == stateId
                      select city).ToList();
            return Json(cities);
        }
    }
}
