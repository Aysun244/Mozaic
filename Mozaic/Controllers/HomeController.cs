using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozaic.DataAccessLayer;
using Mozaic.ViewModelsVM;
using Mozaic.ViewModelsVM.ArchitectVM;
using Mozaic.ViewModelsVM.ProfessionVM;

namespace Mozaic.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async  Task<IActionResult> Index()
        {
            var architects = await _context.Architects.Select(x => new ArchitectGetVM()
            {
                Id=x.Id,
                FullName=x.FullName,
                ImagePath=x.ImagePath,
                Description=x.Description,
                Profession = new()
                {
                    Id= x.Profession.Id,   
                    Name=x.Profession.Name,
                }
            }).ToListAsync();

            var professions = await _context.Professions.Select(x => new ProfessionGetVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            HomeVM vm = new()
            {
                Architects = architects,
                Professions = professions
            };
            return View(vm);
        }
    }
}
