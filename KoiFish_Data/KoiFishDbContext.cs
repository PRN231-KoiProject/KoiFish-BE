using KoiFish_Core.Domain.Content;
using KoiFish_Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data
{
    public class KoiFishDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public KoiFishDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<KoiFish> KoiFishes { get; set; }
        public DbSet<FishPond> FishPonds { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PondFeature> PondFeatures { get; set; }
        public DbSet<FishColor> FishColors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
            .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
            .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty("DateCreated");
                if (entityEntry.State == EntityState.Added
                    && dateCreatedProp != null)
                {
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
