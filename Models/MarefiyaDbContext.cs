using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace MarefiyaApi.Models
{
    public class MarefiyaDbContext:DbContext
    {
        public MarefiyaDbContext(DbContextOptions<MarefiyaDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
            .Property(room => room.RoomNos)
            .HasConversion(x => JsonConvert.SerializeObject(x),
                                   x => JsonConvert.DeserializeObject<int[]>(x));
            

            modelBuilder.Entity<Hotel>()
            .Property(hotel => hotel.Photos)
            .HasConversion(x => JsonConvert.SerializeObject(x), 
                                   x => JsonConvert.DeserializeObject<string[]>(x));
            
            modelBuilder.Entity<HotelDbView>().HasNoKey().ToView("HotelDbView");

            modelBuilder.Entity<HotelDbView>()
            .Property(hotel => hotel.Photos)
            .HasConversion(x => JsonConvert.SerializeObject(x),
                                   x => JsonConvert.DeserializeObject<string[]>(x));


        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelManager> HotelManagers { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews{ get; set; }

    }
}
