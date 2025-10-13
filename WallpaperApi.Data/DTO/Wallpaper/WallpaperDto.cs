using WallpaperApi.Data.DTO.Resolutions;
using WallpaperApi.DTO.Creator;

namespace WallpaperApi.DTO.Wallpaper
{
    public class WallpaperDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? WallpaperTag { get; set; }
        public string FilePath { get; set; }
        public string? Extention { get; set; }
        public int? CreatorId { get; set; }
        public List<ResolutionDTO>? Resolutions { get; set; } = new List<ResolutionDTO>();
    }
}
