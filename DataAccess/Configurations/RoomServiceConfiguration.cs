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
    internal class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
    {
        public void Configure(EntityTypeBuilder<RoomService> builder)
        {
            builder.HasKey(x => new { x.RoomId, x.ServiceId });
        }
    }
}
