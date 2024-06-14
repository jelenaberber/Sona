using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(150);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Images)
                   .HasForeignKey(x => x.RoomId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
