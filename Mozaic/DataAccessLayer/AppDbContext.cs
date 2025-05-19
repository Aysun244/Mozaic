using Microsoft.EntityFrameworkCore;
using Mozaic.Models;

namespace Mozaic.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Architect> Architects { get; set; }
        public DbSet<Profession>Professions { get; set; }

    }
}
