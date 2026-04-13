using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Data.Model
{
    [Table("ReviewProcess")]
    public class ReviewProcess
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? WallpaperTag { get; set; }
        public string FilePath { get; set; }
        public string? Extention { get; set; }
        public string? Status { get; set; }
        public int? CreatorId { get; set; }
        public Creator? Creators { get; set; }
        public List<Resolutions>? Resolutions { get; set; } = new List<Resolutions>();

    }
}
