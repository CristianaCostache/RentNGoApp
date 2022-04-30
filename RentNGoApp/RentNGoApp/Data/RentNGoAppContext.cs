using Microsoft.EntityFrameworkCore;
using RentNGoApp.DataModels;

namespace RentNGoApp.Data
{
    public class RentNGoAppContext : DbContext
    {
        public RentNGoAppContext(DbContextOptions<RentNGoAppContext> options) : base(options) { }

        public DbSet<Car>? cars { get; set; }
        public DbSet<Image>? images { get; set; }
        public DbSet<RentingInfo>? rentingInfos { get; set; }

    }
}
