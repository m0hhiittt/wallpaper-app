using Microsoft.EntityFrameworkCore;
using WallpaperApi.Data.Model;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Wallpaper> Wallpapers { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<ReviewProcess> ReviewProcesses { get; set; }
        public DbSet<Resolutions> Resolutions { get; set; }

    }
}
