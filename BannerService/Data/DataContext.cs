using BannerService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BannerService.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}
