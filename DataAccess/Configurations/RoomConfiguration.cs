using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    internal class RoomConfiguration : EntityConfiguration<Room>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.Capacity).IsRequired();

            builder.Property(x => x.Size).IsRequired();

            builder.Property(x => x.NumberOfUnits).IsRequired();

            builder.Property(x => x.Price)
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

        }
    }
}
