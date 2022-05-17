using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentNGoApp.DataModels;

namespace RentNGoApp.DataAccess
{
    public class RentNGoAppContext : IdentityDbContext<IdentityUser>
    {
        public RentNGoAppContext(DbContextOptions<RentNGoAppContext> options) : base(options) { }

        public DbSet<Car>? cars { get; set; }
        public DbSet<Image>? images { get; set; }
        public DbSet<RentingInfo>? rentingInfos { get; set; }
        public DbSet<User>? users { get; set; }
    }
}
