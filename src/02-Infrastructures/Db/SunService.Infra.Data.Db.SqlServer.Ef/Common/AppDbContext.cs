using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Configurations;
using System.Threading.Tasks;


namespace SunService.Infra.Data.Db.SqlServer.Ef.Common
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityConfigurations());
            modelBuilder.ApplyConfiguration(new ImageConfigurations());
            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerConfigurations());
            modelBuilder.ApplyConfiguration(new ExpertConfigurations());
            modelBuilder.ApplyConfiguration(new ExpertServiceConfigurations());
            modelBuilder.ApplyConfiguration(new HomeServiceConfigurations());
            modelBuilder.ApplyConfiguration(new OfferConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new RatingConfigurations());
          
            modelBuilder.ApplyConfiguration(new SubCategoryConfigurations());
           
            UserConfigurations.SeedUsers(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customers"); 
            modelBuilder.Entity<Expert>().ToTable("Experts"); 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ExpertService> ExpertServices { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
