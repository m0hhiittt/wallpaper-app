using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Data.Model
{
    [Table("Resolutions")]
    public class Resolutions
    {
        public int Id { get; set; }
        public string? Resolution720P { get; set; }
        public string? Resolution1080P { get; set; }
        public string? Resolution2K { get; set; }
        public string? Resolution4K { get; set; }
        public string? Thumbnail { get; set; }
        public int? WallpaperId { get; set; }
        public int? ReviewProcessId { get; set; }
        public Wallpaper? Wallpaper { get; set; }
        public ReviewProcess? ReviewProcess { get; set; }
    }
}
