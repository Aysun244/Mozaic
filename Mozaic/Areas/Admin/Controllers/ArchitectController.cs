using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozaic.DataAccessLayer;
using Mozaic.Models;
using Mozaic.ViewModelsVM.ArchitectVM;
using Mozaic.ViewModelsVM.ProfessionVM;

namespace Mozaic.Areas.Admin.Controllers;
[Area("Admin")]
public class ArchitectController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var architects = await _context.Architects.Select(x => new ArchitectGetVM()
        {
            Id = x.Id,
            ImagePath = x.ImagePath,
            FullName = x.FullName,
            Profession = new()
            {
                Name=x.Profession.Name,
            },
            Description = x.Description,
        }).ToListAsync();
        return View(architects);
    }

    public async Task<IActionResult> Create()
    {
        await ViewBags();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArchitectCreateVM model)
    {
        await ViewBags();
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        if (!model.ImageFile.ContentType.Contains("image"))
        {
            ModelState.AddModelError("ImageFile", "Yalniz sekil formatinda fayl elave oluna biler");
            return View(model);
        }
        if (model.ImageFile.Length / 1024 > 200)
        {
            ModelState.AddModelError("ImageFile", "Seklin tutdugu yer boyukdur");
            return View(model);
        }
        string newFileName = Guid.NewGuid().ToString() + model.ImageFile.FileName;
        string path = Path.Combine("wwwroot", "images", "architect", newFileName);
        using FileStream stream = new(path, FileMode.OpenOrCreate);
        await model.ImageFile.CopyToAsync(stream);
        Architect architect = new Architect()
        {
            FullName = model.FullName,
            Description = model.Description,
            ImagePath = newFileName,
            ProfessionId = model.ProfessionId,
        };

        await _context.Architects.AddAsync(architect);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue || id < 1)
            return BadRequest();

        var architect = await _context.Architects.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (architect == 0) return NotFound();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int? id)
    {
        await ViewBags();

        if (!id.HasValue || id < 1)
            return BadRequest();

        var architect = await _context.Architects.FirstOrDefaultAsync(x => x.Id == id);
        if (architect == null) return NotFound();
        ArchitectUpdateVM model = new ArchitectUpdateVM()
        {
            Id = architect.Id,
            FullName = architect.FullName,
            Description = architect.Description,
            ProfessionId = architect.ProfessionId,
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ArchitectUpdateVM model, int? id)
    {
        await ViewBags();
        if (!id.HasValue || id < 1)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);

        var architect = await _context.Architects.FirstOrDefaultAsync(x => x.Id == id);
        if (architect == null) return NotFound();

        if (model.ImageFile is not null)
        {
            if (!model.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "Yalniz sekil formatinda fayl elave oluna biler");
                return View(model);
            }
            if (model.ImageFile.Length / 1024 > 200)
            {
                ModelState.AddModelError("ImageFile", "Seklin tutdugu yer boyukdur");
                return View(model);
            }
            string newFileName = Guid.NewGuid().ToString() + model.ImageFile.FileName;
            string path = Path.Combine("wwwroot", "images", "architect", newFileName);
            using FileStream stream = new(path, FileMode.OpenOrCreate);
            await model.ImageFile.CopyToAsync(stream);
            architect.ImagePath = newFileName;
        }
        architect.FullName = model.FullName;
        architect.Description = model.Description;
        architect.ProfessionId = model.ProfessionId;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task ViewBags()
    {
        var professions = await _context.Professions.Select(x => new ProfessionGetVM()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToListAsync();
        ViewBag.Professions = professions;
    }
}
