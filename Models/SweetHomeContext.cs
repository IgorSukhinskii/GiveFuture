using Microsoft.EntityFrameworkCore;

namespace SweetHome.Models
{
    public class SweetHomeContext: DbContext
    {
        public SweetHomeContext(DbContextOptions<SweetHomeContext> options)
            : base(options)
        { }

        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<ShelterAnimal> ShelterAnimals { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
