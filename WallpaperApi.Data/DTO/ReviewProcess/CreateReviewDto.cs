using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Wallpaper
{
    public class CreateReviewDto
    {
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}
