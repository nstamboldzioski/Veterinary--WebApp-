using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApp123.Data;
using WebApp123.Models;

namespace WebApp123.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IMemoryCache _memoryCache;

        public VaccinesController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        // GET: Vaccines
        public async Task<IActionResult> Index()
        {
            List<Vaccine> vaccines;
            if (!_memoryCache.TryGetValue("vaccines", out vaccines))
            {
                vaccines = await _context.Vaccine.ToListAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
                cacheOptions.SetPriority(CacheItemPriority.Low);
                cacheOptions.SetSlidingExpiration(new TimeSpan(0, 0, 15));
                cacheOptions.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));

                _memoryCache.Set("vaccines", vaccines, cacheOptions);
            }
            return View(await _context.Vaccine.ToListAsync());
        }

        // GET: Vaccines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                _memoryCache.Remove("vaccines");
                return RedirectToAction(nameof(Index));
            }
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Vaccine vaccine)
        {
            if (id != vaccine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                    _memoryCache.Remove("vaccines");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.Id))
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
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccine == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vaccine'  is null.");
            }
            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccine.Remove(vaccine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
            return (_context.Vaccine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
