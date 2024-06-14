using DataAccess.Configuration;
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
    internal class ReviewConfiguration : EntityConfiguration<Review>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Rate).IsRequired();

            builder.Property(x => x.Comment).IsRequired().HasMaxLength(250);

            builder.HasOne(x => x.User).WithMany(x => x.Reviews)
                                       .HasForeignKey(x => x.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Room).WithMany(x => x.Reviews)
                                       .HasForeignKey(x => x.RoomId)
                                       .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
