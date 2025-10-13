using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WallpaperApi_Model.Model
{
    [Table("Wallpaper")]
    public class Wallpaper
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string? Title { get; set; }
        public string? FilePath { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string? FileName { get; set; }
        public string? Resolution { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
