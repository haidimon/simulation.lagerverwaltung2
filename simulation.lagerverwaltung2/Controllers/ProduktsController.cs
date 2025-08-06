using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simulation.lagerverwaltung2.Data;
using simulation.lagerverwaltung2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simulation.lagerverwaltung2.Controllers
{
    public class ProduktsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produkts
        public async Task<IActionResult> Index()
        {
            var produkte = _context.Produkte
                .Include(p => p.Kategorie)
                .Include(p => p.Lager)
                .ToListAsync();

            return View(await produkte);
        }


        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkte
                .Include(p => p.Kategorie)
                .Include(p => p.Lager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkts/Create
        public IActionResult Create()
        {
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "Id", "Name");
            ViewData["LagerId"] = new SelectList(_context.Lager, "Id", "Name");
            return View();
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Anzahl,KategorieId,LagerId")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "Id", "Name", produkt.KategorieId);
            ViewData["LagerId"] = new SelectList(_context.Lager, "Id", "Name", produkt.LagerId);
            return View(produkt);
        }

        // GET: Produkts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkte.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "Id", "Id", produkt.KategorieId);
            ViewData["LagerId"] = new SelectList(_context.Lager, "Id", "Id", produkt.LagerId);
            return View(produkt);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Anzahl,KategorieId,LagerId")] Produkt produkt)
        {
            if (id != produkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.Id))
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
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "Id", "Id", produkt.KategorieId);
            ViewData["LagerId"] = new SelectList(_context.Lager, "Id", "Id", produkt.LagerId);
            return View(produkt);
        }

        // GET: Produkts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkte
                .Include(p => p.Kategorie)
                .Include(p => p.Lager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkte.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkte.Remove(produkt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkte.Any(e => e.Id == id);
        }
    }
}
