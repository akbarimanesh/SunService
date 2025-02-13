

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class HomeServiceConfigurations : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.BasePrice).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();

            builder.HasMany(x => x.orders)
               .WithOne(x => x.HomeService)
               .HasForeignKey(x => x.HomeServiceId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasData(new List<HomeService>()
            {
                new HomeService() { Id = 1, Title = "سرویس نظافت فوری",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=2000000,NumberVisits=202,ImagePath = "\\Images\\Homeservice\\Cleaning-and-catering-homeservice_image.jpg",SubCategoryId=1},
                new HomeService() { Id = 2, Title = "قالیشویی",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=3500000,NumberVisits=145,ImagePath = "\\Images\\Homeservice\\Washing-homeservice_image.jpg",SubCategoryId=2 },
                new HomeService() { Id = 3, Title = "نجاری",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=4000000,NumberVisits=128,ImagePath = "\\Images\\Homeservice\\Cabinet-homeservice_image.jpg",SubCategoryId=3  },
                new HomeService() { Id = 4, Title = "نصب و تعمیر شیرآلات",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=2500000,NumberVisits=235,ImagePath = "\\Images\\Homeservice\\Plumbing-homeservice_image.jpg",SubCategoryId=4 },
                new HomeService() { Id = 5, Title = "نصب و تعمیر ماشین لباسشویی",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=1000000,NumberVisits=147,ImagePath = "\\Images\\Homeservice\\Home-appliances-homeservice_image.jpg",SubCategoryId=5},
                new HomeService() { Id = 6, Title = "تعمیر کولر گازی",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=1000000,NumberVisits=147,ImagePath = "\\Images\\Homeservice\\Cooling and heating-homeservice_image.jpg",SubCategoryId=6},
                new HomeService() { Id = 7, Title = "سرویس بسته بندی",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=4000000,NumberVisits=117,ImagePath = "\\Images\\Homeservice\\Freight-homeservice_image.jpg",SubCategoryId=7},
                new HomeService() { Id = 8, Title = "امداد خودرو",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=2500000,NumberVisits=149,ImagePath = "\\Images\\Homeservice\\Car-repair-homeservice_image.jpg",SubCategoryId=8},
                new HomeService() { Id = 9, Title = "کارواش با آب",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=3500000,NumberVisits=122,ImagePath = "\\Images\\Homeservice\\Car-wash-homeservice_image.jpg",SubCategoryId=9},
                new HomeService() { Id = 10, Title = "پرستاری و تزریقات",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=4000000,NumberVisits=224,ImagePath = "\\Images\\Homeservice\\Medicine-and-nursing-homeservice_image.jpg",SubCategoryId=10},
                new HomeService() { Id = 11, Title = "زیبایی بانوان",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=5000000,NumberVisits=149,ImagePath = "\\Images\\Homeservice\\Women's-beauty-homeservice_image.jpg",SubCategoryId=11},
                new HomeService() { Id = 12, Title = "خدمات شرکتی ویژه شرکت های کوچک",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=2200000,NumberVisits=110,ImagePath = "\\Images\\Homeservice\\Corporate services-homeservice_image.jpg",SubCategoryId=12},
                new HomeService() { Id = 13, Title = "استخدام خدمتکار",Description="لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با استفاده از طراحان گرافیک است",BasePrice=3400000,NumberVisits=132,ImagePath = "\\Images\\Homeservice\\Providing-human-resources-homeservice_image.jpg",SubCategoryId=13}
                   


            });
        }
    }
}
