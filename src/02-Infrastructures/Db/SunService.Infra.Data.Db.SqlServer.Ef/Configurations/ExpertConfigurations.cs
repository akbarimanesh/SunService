

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class ExpertConfigurations : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {

            builder.Property(c => c.Biography).IsRequired().HasMaxLength(2000);

            builder.HasMany(x => x.Orders)
               .WithOne(x => x.Expert)
               .HasForeignKey(x => x.ExpertId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
