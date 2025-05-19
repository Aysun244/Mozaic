namespace Mozaic.Models
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Architect>? Architects { get; set; }
    }
}
