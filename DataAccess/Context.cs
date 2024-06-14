
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain;
using DataAccess.Configuration;

namespace DataAccess
{
    public class Context : DbContext
    {
        private readonly string _connectionString;
        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Context()
        {
            _connectionString = "Data Source=DESKTOP-HS2JCR0\\SQLEXPRESS;Initial Catalog=Sona;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<UserUseCase>().HasKey(x => new
            {
                x.UserId,
                x.UseCaseId
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();


            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RestaurantService> RestaurantServices { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRoom> BookingRooms { get; set; }
        public DbSet<UseCaseAuditLog> UseCaseLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

    }
}
