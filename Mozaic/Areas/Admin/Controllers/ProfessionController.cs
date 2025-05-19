using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozaic.DataAccessLayer;
using Mozaic.Models;
using Mozaic.ViewModelsVM.ArchitectVM;
using Mozaic.ViewModelsVM.ProfessionVM;

namespace Mozaic.Areas.Admin.Controllers;
[Area("Admin")]
public class ProfessionController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var professions = await _context.Professions.Select(x => new ProfessionGetVM()
        {
            Id = x.Id,         
            Name = x.Name,
           
        }).ToListAsync();
        return View(professions);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProfessionCreateVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Profession profession = new Profession()
        {
            Name = model.Name,         
        };

        await _context.Professions.AddAsync(profession);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue || id < 1)
            return BadRequest();

        var architect = await _context.Professions.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (architect == 0) return NotFound();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int? id)
    {

        if (!id.HasValue || id < 1)
            return BadRequest();

        var profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == id);
        if (profession == null) return NotFound();
        ProfessionUpdateVM model = new ProfessionUpdateVM()
        {
            Id = profession.Id,          
            Name = profession.Name,
           
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProfessionUpdateVM model, int? id)
    {
        if (!id.HasValue || id < 1)
            return BadRequest();

        var profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == id);
        if (profession == null) return NotFound();

        profession.Name = model.Name;
        
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
