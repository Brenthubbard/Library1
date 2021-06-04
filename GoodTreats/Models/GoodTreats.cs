using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoodTreats.Models
{
  public class GoodTreatsContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Flavor> Flavor { get; set; }
    public DbSet<Treat> Treats { get; set; }

    public DbSet<TreatFlavor> TreatFlavor { get; set; }

    public GoodTreatsContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}