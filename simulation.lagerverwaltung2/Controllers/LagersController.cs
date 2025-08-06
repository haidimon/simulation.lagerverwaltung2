using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simulation.lagerverwaltung2.Data;
using simulation.lagerverwaltung2.Models;
using System.Linq;
using System.Threading.Tasks;


public class LagersController : Controller
{
    private readonly ApplicationDbContext _context;

    public LagersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Index als Hauptseite für Lagerverwaltung (Admin + User)
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Index()
    {
       
        var lagerList = await _context.Lager.ToListAsync();
        return View(lagerList); // Sucht nach Views/Lagers/Index.cshtml
    }

    // Create: nur Admin
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Lager lager)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        return View(lager);
    }

    // Edit: nur Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var lager = await _context.Lager.FindAsync(id);
        if (lager == null) return NotFound();

        return View(lager);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Lager lager)
    {
        if (id != lager.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lager);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LagerExists(lager.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(lager);
    }

    // Delete: nur Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var lager = await _context.Lager.FirstOrDefaultAsync(m => m.Id == id);
        if (lager == null) return NotFound();

        return View(lager);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lager = await _context.Lager.FindAsync(id);
        if (lager != null)
        {
            _context.Lager.Remove(lager);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool LagerExists(int id)
    {
        return _context.Lager.Any(e => e.Id == id);
    }
}
