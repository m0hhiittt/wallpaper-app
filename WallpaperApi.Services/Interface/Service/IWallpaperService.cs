using System.Drawing.Printing;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services.Interface.Service
{
    public interface IWallpaperService
    {
        Task<Wallpaper> UploadWallpaperService(CreateWallpaperDto wallpaperDto);
        Task<Wallpaper> UpdateWallpaperService(int id, UpdateWallpaperDto fileModel);
        Task<Wallpaper> DeleteWallpaperService(int id);
        Task<Wallpaper> GetWallpaperByIdService(int id);
        Task<(IEnumerable<Wallpaper>, int)> GetAllWallpapersService(int pageNumber, int pageSize);
    }
}
