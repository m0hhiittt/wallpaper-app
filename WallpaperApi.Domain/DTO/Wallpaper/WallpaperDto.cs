

using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Wallpaper
{
    public class WallpaperDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Title { get; set; }
        public string FilePath { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FileName { get; set; }
        public string Resolution { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
