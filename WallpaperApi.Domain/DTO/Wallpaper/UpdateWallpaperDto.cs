using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Wallpaper
{
    public class UpdateWallpaperDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Title { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
