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
    internal class BookingConfiguration : EntityConfiguration<Booking>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(x => x.TravelingForWork).IsRequired();

            builder.Property(x => x.NumberOfAdults).IsRequired();

            builder.Property(x => x.NumberOfChildren).IsRequired();

            builder.Property(x => x.GestName).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.GestName);

            builder.Property(x => x.Request).HasMaxLength(200);

            builder.Property(x => x.CheckIn).IsRequired(); 

            builder.Property(x => x.CheckOut).IsRequired();

            builder.Property(x => x.FinalPrice).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.User).WithMany(x => x.Bookings)
                                       .HasForeignKey(x => x.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RestaurantService).WithMany(x => x.Bookings)
                                       .HasForeignKey(x => x.RestaurantServiceId)
                                       .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
