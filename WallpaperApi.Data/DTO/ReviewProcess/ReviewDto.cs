

using System.ComponentModel.DataAnnotations;
using WallpaperApi.Data.DTO.Resolutions;
using WallpaperApi.DTO.Creator;

namespace WallpaperApi.DTO.Wallpaper
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string? WallpaperTag { get; set; } 
        public string FilePath { get; set; }
        public string? Extention { get; set; }
        public string? Status { get; set; }
        public int? CreatorId { get; set; }
        public CreatorDto? Creators { get; set; }
        public List<ResolutionDTO>? Resolutions { get; set; } = new List<ResolutionDTO>();
    }
}
