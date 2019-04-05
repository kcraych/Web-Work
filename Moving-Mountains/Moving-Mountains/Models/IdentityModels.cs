using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Moving_Mountains.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MountainsContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<UserDemographic> UserDemographics { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ActivityDetail> ActivityDetails { get; set; }
        public DbSet<ActivityComplete> ActivityCompletes { get; set; }
        public DbSet<ActivityPhoto> ActivityPhotos { get; set; }
        public DbSet<ActivityWishList> ActivityWishLists { get; set; }
        public DbSet<TrailDetail> TrailDetails { get; set; }
        public DbSet<GovernedLand> GovernedLands { get; set; }
        public DbSet<MountainRange> MountainRanges { get; set; }
    }
}