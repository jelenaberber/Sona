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
    internal class BookingRoomConfiguration : IEntityTypeConfiguration<BookingRoom>
    {
        public void Configure(EntityTypeBuilder<BookingRoom> builder)
        {
            builder.Property(x => x.quantity).IsRequired();

            builder.HasKey(x => new { x.RoomId, x.BookingId });
        }
    }
}
