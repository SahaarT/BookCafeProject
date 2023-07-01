using Microsoft.EntityFrameworkCore;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace BulkyBook.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        optionsBuilder.UseSqlServer("Server=DESKTOP-9P51JRG;Database=BulkyBook;Trusted_Connection=True");

    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
}
