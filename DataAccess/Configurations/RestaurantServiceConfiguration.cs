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
    internal class RestaurantServiceConfiguration : EntityConfiguration<RestaurantService>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RestaurantService> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(60)
                   .IsRequired();
            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.Property(x => x.Description)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.PricePerDay)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

        }
    }
}
