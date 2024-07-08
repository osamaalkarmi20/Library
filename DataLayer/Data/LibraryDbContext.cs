using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        public DbSet<LookUp> LookUps { get; set; }
        public DbSet<LookUpCategory> LookUpCategories { get; set; }
        public DbSet<Book> Books  { get; set; }
        public DbSet<Shelf> Shelfs  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed LookUpCategories
            modelBuilder.Entity<LookUpCategory>().HasData(
                new LookUpCategory { Id = 1, Name = "TypeOfShelf", Code = "1", CreationDate = DateTime.UtcNow }
            );

            // Seed LookUps
            modelBuilder.Entity<LookUp>().HasData(
                new LookUp { Id = 1, LookUpCategoryId = 1, Name = "Fantasy", Code = "FAN", CreationDate = DateTime.UtcNow },
                new LookUp { Id = 2, LookUpCategoryId = 1, Name = "Novel", Code = "NOV", CreationDate = DateTime.UtcNow },
                new LookUp { Id = 3, LookUpCategoryId = 1, Name = "History", Code = "HIS", CreationDate = DateTime.UtcNow },
                new LookUp { Id = 4, LookUpCategoryId = 1, Name = "Medical", Code = "MED", CreationDate = DateTime.UtcNow }
            );
        }
    }
}