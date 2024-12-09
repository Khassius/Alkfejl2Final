using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PetRegistryApp.Context;
using PetRegistryApp.Models;

namespace PetRegistryApp.Controllers
{
    public class PetsController : Controller
    {
        private readonly EFContext _context;

        public PetsController(EFContext context)
        {
            _context = context;
        }

		// GET: Pets
		public async Task<IActionResult> Index(string? SortOrder, int? CategoryIDFilter, PetType? PetTypeFilter,
												string? NameFilter, bool? RetiredFilter)
		{
			var efContext = _context.Pets.Include(p => p.ReferencedCategory);
			var res = efContext.AsQueryable();

			//Console.WriteLine(SortOrder + " " + CategoryIDFilter + " " + NameFilter + " " + RetiredFilter);

			if (SortOrder != null && SortOrder.Equals("up"))
			{
				res = res.OrderByDescending(e => e.Name);
			}
			if (SortOrder != null && SortOrder.Equals("down"))
			{
				res = res.OrderBy(e => e.Name);
			}
			if (CategoryIDFilter != null)
			{
				Console.WriteLine("category read");
				res = res.Where(e => e.CategoryID.Equals(CategoryIDFilter));
			}
			if (PetTypeFilter != null)
			{
				res = res.Where(e => e.PetType.Equals(PetTypeFilter));
			}
			if (NameFilter != null)
			{
				res = res.Where(e => e.Name.ToLower().Contains(NameFilter.ToLower()));
			}
			if (RetiredFilter != null)
			{
				res = res.Where(e => e.Retired == RetiredFilter);
			}
			ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName");
			return View(await res.ToListAsync());
		}

		// GET: Pets/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.ReferencedCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PetType,Retired,ContractExp,Gender,Age,Weight,PhotoURL,CategoryID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", pet.CategoryID);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", pet.CategoryID);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,PetType,Retired,ContractExp,Gender,Age,Weight,PhotoURL,CategoryID")] Pet pet)
        {
            if (id != pet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", pet.CategoryID);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.ReferencedCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.ID == id);
        }
    }
}
