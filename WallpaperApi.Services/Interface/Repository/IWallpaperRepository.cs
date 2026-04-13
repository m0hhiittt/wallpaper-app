using WallpaperApi.DTO.Wallpaper;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services.Interface.Repository
{
    public interface IWallpaperRepository
    {
        Task<Wallpaper> GetWallpaperByIdAsync(int id);
        Task CreateWallpaperAsync(Wallpaper wallpaper);
        Task UpdateWallpaperAsync(UpdateWallpaperDto wallpaperDto, int id);
        Task DeleteWallpaperAsync(int id);
        Task<IEnumerable<Wallpaper>> GetAllWallpapersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalWallpapersCountAsync();
    }
}
