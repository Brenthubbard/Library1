using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GoodTreats.Models
{
  public class GoodTreatsContextFactory : IDesignTimeDbContextFactory<GoodTreatsContext>
  {

    GoodTreatsContext IDesignTimeDbContextFactory<GoodTreatsContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<GoodTreatsContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new GoodTreatsContext(builder.Options);
    }
  }
}