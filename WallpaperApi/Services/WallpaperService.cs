using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services
{
    public class WallpaperService : IWallpaperService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IWallpaperRepository _wallpaperRepo;

        public WallpaperService(IWallpaperRepository wallpaperRepo, IWebHostEnvironment env)
        {
            _wallpaperRepo = wallpaperRepo;
            _env = env;
        }

        public async Task<Wallpaper?> DeleteWallpaperService(int id)
        {
            // 1. Get wallpaper with resolutions
            var exist = await _wallpaperRepo.GetWallpaperByIdAsync(id);

            if (exist == null)
                return null;

            // 2. Build base path
            var contentPath = _env.ContentRootPath;
            var uploadPath = Path.Combine(contentPath, "uploads");

            // 3. Delete original file
            var originalPath = Path.Combine(uploadPath, exist.FilePath);
            if (File.Exists(originalPath))
            {
                File.Delete(originalPath);
            }

            // 4. Delete all resolutions
            foreach (var res in exist.Resolutions)
            {
                var filesToDelete = new List<string?>
            {
                res.Resolution720P,
                res.Resolution1080P,
                res.Resolution2K,
                res.Resolution4K,
                res.Thumbnail
            };

                foreach (var file in filesToDelete)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        var filePath = Path.Combine(uploadPath, file);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }
            }

            await _wallpaperRepo.DeleteWallpaperAsync(id);
            return exist;
        }


        public async Task<(IEnumerable<Wallpaper>, int)> GetAllWallpapersService(int pageNumber, int pageSize)
        {
            var wallpapers = await _wallpaperRepo.GetAllWallpapersAsync(pageNumber, pageSize);
            var totalCount = await _wallpaperRepo.GetTotalWallpapersCountAsync();
            return (wallpapers, totalCount);
        }

        public Task<Wallpaper> GetWallpaperByIdService(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Wallpaper> UpdateWallpaperService(int id, UpdateWallpaperDto fileModel)
        {
            throw new NotImplementedException();
        }

        public Task<Wallpaper> UploadWallpaperService(CreateWallpaperDto wallpaperDto)
        {
            throw new NotImplementedException();
        }
    }
}