using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;
using WebApp123.Data;
using WebApp123.Models;

namespace WebApp123.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IMemoryCache _memoryCache;

        public PetsController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            List<Pet> pets;
            if (!_memoryCache.TryGetValue("pets", out pets))
            {
                pets = await _context.Pet.ToListAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
                cacheOptions.SetPriority(CacheItemPriority.Low);
                cacheOptions.SetSlidingExpiration(new TimeSpan(0, 0, 15));
                cacheOptions.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));

                _memoryCache.Set("pets", pets, cacheOptions);
            }
            var applicationDbContext = _context.Pet.Include(p => p.Person).Include(p => p.Vaccines);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pet == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.Person)
                .Include(p => p.Vaccines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name");
            ViewData["VaccineId"] = new MultiSelectList(_context.Vaccine, "Id", "Name");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,PersonId,VaccinesParams")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                var vaccines = _context.Vaccine.Where(x => pet.VaccinesParams.Contains(x.Id)).ToList();
                pet.Vaccines.AddRange(vaccines);
                _context.Add(pet);
                await _context.SaveChangesAsync();
                _memoryCache.Remove("pets");
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name", pet.PersonId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pet == null)
            {
                return NotFound();
            }
            var pet = await _context.Pet.Include(s => s.Vaccines).Include(m => m.Vaccines).FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name", pet.PersonId);
            ViewData["VaccineId"] = new MultiSelectList(_context.Vaccine, "Id", "Name", pet.Vaccines.Select(x => x.Id));
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,PersonId,VaccinesParams")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPet = await _context.Pet.Include(p => p.Vaccines).FirstOrDefaultAsync(p => p.Id == id);
                    existingPet.Name = pet.Name;
                    existingPet.Age = pet.Age;
                    existingPet.PersonId = pet.PersonId;
                    if (pet.VaccinesParams != null)
                    {
                        existingPet.Vaccines.Clear();
                        foreach (var vaccineId in pet.VaccinesParams)
                        {
                            var vaccine = await _context.Vaccine.FindAsync(vaccineId);
                            if (vaccine != null)
                            {
                                existingPet.Vaccines.Add(vaccine);
                            }
                        }
                    }
                    _context.Update(existingPet);
                    await _context.SaveChangesAsync();
                    _memoryCache.Remove("pets");   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name", pet.PersonId);
            ViewBag.VaccineList = new MultiSelectList(_context.Vaccine, "Id", "Name", pet.Vaccines.Select(v => v.Id));
            return View(pet);
        }

        // GET: Pets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pet == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pet'  is null.");
            }
            var pet = await _context.Pet.FindAsync(id);
            if (pet != null)
            {
                _context.Pet.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return (_context.Pet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
