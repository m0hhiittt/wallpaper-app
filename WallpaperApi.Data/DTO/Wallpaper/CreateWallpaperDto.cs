using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Wallpaper
{
    public class CreateWallpaperDto
    {
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public IFormFile File { get; set; }

    }
}
