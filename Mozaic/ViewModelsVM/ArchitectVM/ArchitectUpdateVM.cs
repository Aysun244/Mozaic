namespace Mozaic.ViewModelsVM.ArchitectVM
{
    public class ArchitectUpdateVM
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public int ProfessionId { get; set; }
    }
}
