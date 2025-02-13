

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class SubCategoryConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.HomeServices)
                .WithOne(x => x.SubCategory)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new List<SubCategory>()
            {
                new SubCategory() { Id = 1, Title = "نظافت و پذیرایی",CategoryId=1},
                new SubCategory() { Id = 2, Title = "شستشو",CategoryId=1 },
                new SubCategory() { Id = 3, Title = "چوب و کابینت",CategoryId=2},
                new SubCategory() { Id = 4, Title = "لوله کشی",CategoryId=2},
                new SubCategory() { Id = 5, Title = "نصب و تعمیر لوازم خانگی",CategoryId=3},
                new SubCategory() { Id = 6, Title = "سرمایش و گرمایش",CategoryId=3},
                new SubCategory() { Id = 7, Title = "باربری و جابه جایی",CategoryId=4 },
            
                new SubCategory() { Id = 8, Title = "خدمات و تعمیرات خودرو",CategoryId=5 },
                new SubCategory() { Id = 9, Title = "کارواش خودرو",CategoryId=5 },
                new SubCategory() { Id = 10, Title = "پزشکی و پرستاری",CategoryId=6 },
                new SubCategory() { Id = 11, Title = "زیبایی بانوان",CategoryId=6 },
                new SubCategory() { Id = 12, Title = "خدمات شرکتی",CategoryId=7 },
                new SubCategory() { Id = 13, Title = "تامین نیروی انسانی",CategoryId=7 }



            });
        }
    }
}
