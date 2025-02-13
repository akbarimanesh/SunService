

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.orders)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new List<City>()
            {
                new City() { Id = 1, Title = "تهران" },
                new City() { Id = 2, Title = "مشهد" },
                new City() { Id = 3, Title = "اصفهان" },
                new City() { Id = 4, Title = "شیراز" },
                new City() { Id = 5, Title = "تبریز" },
                new City() { Id = 6, Title = "کرج" },
                new City() { Id = 7, Title = "قم" },
                new City() { Id = 8, Title = "اهواز" },
                new City() { Id = 9, Title = "رشت" },
                new City() { Id = 10, Title = "کرمانشاه" },
                new City() { Id = 11, Title = "زاهدان" },
                new City() { Id = 12, Title = "ارومیه" },
                new City() { Id = 13, Title = "یزد" },
                new City() { Id = 14, Title = "همدان" },
                new City() { Id = 15, Title = "قزوین" },
                new City() { Id = 16, Title = "سنندج" },
                new City() { Id = 17, Title = "بندرعباس" },
                new City() { Id = 18, Title = "زنجان" },
                new City() { Id = 19, Title = "ساری" },
                new City() { Id = 20, Title = "بوشهر" },
                new City() { Id = 21, Title = "بجنورد" },
                new City() { Id = 22, Title = "گرگان" },
                new City() { Id = 23, Title = "کرمان" },
                new City() { Id = 24, Title = "خرم آباد" },
                new City() { Id = 25, Title = "سمنان" },
                new City() { Id = 26, Title = "بیرجند" },
                new City() { Id = 27, Title = "اراک" },
                new City() { Id = 28, Title = "اردبیل" },
                new City() { Id = 29, Title = "ایلام" },
                new City() { Id = 30, Title = "شهرکرد" },
                new City() { Id = 31, Title = "یاسوج" },
               
            });
        }
    }
   
}
