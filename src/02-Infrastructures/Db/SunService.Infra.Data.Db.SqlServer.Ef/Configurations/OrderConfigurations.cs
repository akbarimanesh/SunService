

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
           
            builder.Property(x => x.HomeServiceId).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.ImplementationDate).IsRequired();
            builder.Property(x => x.ImplementationTime).IsRequired();
        }
    }
}
