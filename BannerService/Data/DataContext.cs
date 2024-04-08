using BannerService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BannerService.Data
{
    public class DataContext : DbContext
    { 
        public DbSet<User> Users { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Feature> Features { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Assembly.GetAssembly(typeof(DataContext)).Location;
            path = Path.GetDirectoryName(path);
            optionsBuilder.UseSqlite($"Data Source = {Path.Combine(path, "data.db")}");
        }
    }
}
