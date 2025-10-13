using Microsoft.EntityFrameworkCore;
using WallpaperApi.Data;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Repository
{
    public class WallpaperRepository : IWallpaperRepository
    {
        private readonly ApplicationDbContext _context;

        public WallpaperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallpaper> GetWallpaperByIdAsync(int id)
        {
                var wallpaper = await _context.Wallpapers
                .Include(w => w.Resolutions)
                .FirstOrDefaultAsync(w => w.Id == id);
            return wallpaper;
        }


        public async Task CreateWallpaperAsync(Wallpaper wallpaper)
        {
            _context.Wallpapers.Add(wallpaper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWallpaperAsync(UpdateWallpaperDto wallpaperDto, int id)
        {
            var wallpaper = await _context.Wallpapers.FindAsync(id);
            if (wallpaper != null)
            {
                wallpaper.Title = wallpaperDto.Title;

                _context.Wallpapers.Update(wallpaper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteWallpaperAsync(int id)
        {
            var wallpaper = await _context.Wallpapers.FindAsync(id);
            if (wallpaper != null)
            {
                _context.Wallpapers.Remove(wallpaper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Wallpaper>> GetAllWallpapersAsync(int pageNumber, int pageSize)
        {
            return await _context.Wallpapers
         .Include(w => w.Resolutions)
         .Skip((pageNumber - 1) * pageSize) // skip previous pages
         .Take(pageSize)                    // take only required count
         .ToListAsync();
        }
        public async Task<int> GetTotalWallpapersCountAsync()
        {
            return await _context.Wallpapers.CountAsync();
        }
    }
}
