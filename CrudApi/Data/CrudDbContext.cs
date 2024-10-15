using CrudApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Data
{
	public class CrudDbContext:DbContext
	{
        public CrudDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
