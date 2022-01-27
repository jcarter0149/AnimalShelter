using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Web
{
    public class DbContext : AbstractDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
