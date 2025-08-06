using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simulation.lagerverwaltung2.Data;
using simulation.lagerverwaltung2.Models;
using System.Threading.Tasks;


public class KategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public KategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Für Admin + User: Kategorien anzeigen (Liste)
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var kategorien = await _context.Kategorie.ToListAsync();
        return View(kategorien);
    }

    // Für Admin + User: Details einer Kategorie anzeigen
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var kategorie = await _context.Kategorie.FirstOrDefaultAsync(m => m.Id == id);
        if (kategorie == null) return NotFound();

        return View(kategorie);
    }

    // Nur für Admin: Kategorie erstellen
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Kategorie kategorie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(kategorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(kategorie);
    }

    // Nur für Admin: Kategorie bearbeiten
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var kategorie = await _context.Kategorie.FindAsync(id);
        if (kategorie == null) return NotFound();

        return View(kategorie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Kategorie kategorie)
    {
        if (id != kategorie.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(kategorie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorieExists(kategorie.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(kategorie);
    }

    // Nur für Admin: Kategorie löschen
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var kategorie = await _context.Kategorie.FirstOrDefaultAsync(m => m.Id == id);
        if (kategorie == null) return NotFound();

        return View(kategorie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var kategorie = await _context.Kategorie.FindAsync(id);
        if (kategorie != null)
        {
            _context.Kategorie.Remove(kategorie);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool KategorieExists(int id)
    {
        return _context.Kategorie.Any(e => e.Id == id);
    }
}
