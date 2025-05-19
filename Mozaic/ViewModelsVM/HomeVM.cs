using Mozaic.ViewModelsVM.ArchitectVM;
using Mozaic.ViewModelsVM.ProfessionVM;

namespace Mozaic.ViewModelsVM
{
    public class HomeVM
    {
        public List<ArchitectGetVM> Architects { get; set; }
        public List<ProfessionGetVM> Professions { get; set; }
    }
}
