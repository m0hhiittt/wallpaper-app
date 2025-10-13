using WallpaperApi.Data.DTO.Resolutions;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Mapper;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Mapper
{
    public static class WallpaperMapper
    {
        public static WallpaperDto ToWallpaperDto(this Wallpaper model)
        {
            return new WallpaperDto
            {
                Id = model.Id,
                Title = model.Title,
                FilePath = model.FilePath,
                WallpaperTag = model.WallpaperTag,
                Extention = model.Extention,
                CreatorId = model.CreatorId,
                Resolutions = model.Resolutions.Select(ResolutionMapper.ToResolutionMapper).ToList(),
            };
        }

        public static Wallpaper ToWallpaperFromCreate(this CreateWallpaperDto model, string path)
        {
            return new Wallpaper
            {
                CreatorId = model.CreatorId,
                Title= model.Title,
                FilePath= path,
            };
        }

        public static Wallpaper ToWallpaperFromUpdate(this UpdateWallpaperDto model, string path)
        {
            return new Wallpaper
            {
                CreatorId = model.CreatorId,
                Title = model.Title,
                FilePath = path,
            };
        }
    }
}
