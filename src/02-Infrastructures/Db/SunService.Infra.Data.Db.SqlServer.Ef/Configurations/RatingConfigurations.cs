
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System.Reflection.Emit;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class RatingConfigurations : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x=> x.Score)
            .IsRequired();
            builder.HasOne(x => x.Expert)
           .WithMany(x=>x.Ratings)
           .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Customer)
           .WithMany(x => x.Ratings)
           .HasForeignKey(r => r.CustomerId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.HomeService)
           .WithMany(x => x.Ratings)
           .HasForeignKey(r => r.HomeServiceId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
