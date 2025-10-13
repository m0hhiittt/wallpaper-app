using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WallpaperApi.Data.Model;

namespace WallpaperApi_Model.Model
{
    [Table("Wallpaper")]
    public class Wallpaper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? WallpaperTag { get; set; }
        public string FilePath { get; set; }
        public string? Extention { get; set; }
        public int? CreatorId { get; set; }
        public List<Resolutions>? Resolutions { get; set; } = new List<Resolutions>();
    }
}
