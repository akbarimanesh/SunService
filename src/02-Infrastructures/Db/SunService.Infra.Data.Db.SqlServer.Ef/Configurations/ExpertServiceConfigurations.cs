using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunService.Domain.Core.SunServices.HService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class ExpertServiceConfigurations : IEntityTypeConfiguration<ExpertService>
    {
        public void Configure(EntityTypeBuilder<ExpertService> builder)
        {
            builder.HasKey(x => new { x.ExpertId, x.HomeServiceId });

            builder.HasOne(x => x.HomeService)
                .WithMany(x => x.ExpertServices)
                .HasForeignKey(x => x.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Expert)
                .WithMany(x => x.ExpertServices)
                .HasForeignKey(x => x.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
