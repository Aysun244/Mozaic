using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Mozaic.Models;
using Mozaic.ViewModelsVM.ProfessionVM;


namespace Mozaic.ViewModelsVM.ArchitectVM
{
    public class ArchitectGetVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; } 
        public ProfessionGetVM Profession { get; set; }
      
    }
}
