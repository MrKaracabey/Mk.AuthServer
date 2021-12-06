using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mk.AuthServer.Core.models;

namespace Mk.AuthServer.API.Data
{
    public class EfDataContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public EfDataContext(DbContextOptions<EfDataContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
            
        }
    }
}