using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseAuditLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseAuditLog> builder)
        {
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);

            builder.Property(x => x.UseCaseName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.ExecutedAt).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => new { x.Username, x.UseCaseName, x.ExecutedAt })
                   .IncludeProperties(x => x.UseCaseData);
        }
    }
}
