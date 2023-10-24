using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ForumApi.Data
{
    /// <summary>
    /// used by dotnet ef
    /// </summary>
    public class ForumDbContextFactory : IDesignTimeDbContextFactory<ForumDbContext>
    {
        public ForumDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ForumDbContext>();

            string workingDirectory = Environment.CurrentDirectory;

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile($"{workingDirectory}\\appsettings.json");
            IConfigurationRoot config = builder.Build();

            string? connectionString = config.GetConnectionString("ForumDb");
            optionsBuilder.UseNpgsql(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            return new ForumDbContext(optionsBuilder.Options);
        }
    }
}