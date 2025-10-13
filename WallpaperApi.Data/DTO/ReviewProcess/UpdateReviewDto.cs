using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Wallpaper
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
