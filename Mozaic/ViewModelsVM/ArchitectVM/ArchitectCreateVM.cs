namespace Mozaic.ViewModelsVM.ArchitectVM
{
    public class ArchitectCreateVM
    {      
        public string FullName { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public int ProfessionId { get; set; }
    }
}
