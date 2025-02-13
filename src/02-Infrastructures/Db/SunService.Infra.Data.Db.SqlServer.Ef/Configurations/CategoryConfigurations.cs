using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;


namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

           

            builder.HasData(new List<Category>()
            {
                new Category() { Id = 1, Title = "تمیزکاری",ImagePath = "\\Images\\Category\\clean-mainCategory-icon.jpg"},
                new Category() { Id = 2, Title = "ساختمان",ImagePath = "\\Images\\Category\\Building-mainCategory-icon.jpg" },
                new Category() { Id = 3, Title = "تعمیرات اشیا",ImagePath = "\\Images\\Category\\Repairs-mainCategory-icon.jpg" },
                new Category() { Id = 4, Title = "اسباب کشی و حمل بار",ImagePath = "\\Images\\Category\\Cargo-transportation-mainCategory-icon.jpg"},
                new Category() { Id = 5, Title = "خودرو",ImagePath = "\\Images\\Category\\Car-mainCategory-icon.jpg"},
                new Category() { Id = 6, Title = "سلامت و زیبایی",ImagePath = "\\Images\\Category\\Beauty-mainCategory-icon.jpg"},
                new Category() { Id = 7, Title = "سازمان ها و مجتمع ها",ImagePath = "\\Images\\Category\\Complexes-mainCategory-icon.jpg"},

                

            });
        }
    }
}
