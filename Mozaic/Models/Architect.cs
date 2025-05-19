namespace Mozaic.Models
{
    public class Architect : BaseEntity
    {
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }

      
    }
}
