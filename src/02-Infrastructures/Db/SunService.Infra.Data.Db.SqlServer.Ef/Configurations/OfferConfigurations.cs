

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class OfferConfigurations : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.PriceOffer).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Order)
               .WithMany(x => x.Offers)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Expert)
               .WithMany(x => x.Offers)
               .HasForeignKey(x => x.ExpertId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
