using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WallpaperApi.Data.Model;

namespace WallpaperApi_Model.Model
{
    [Table("Creator")]
    public class Creator
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name can't be exceed 10 words")]
        [MinLength(2)]
        [MaxLength(10)]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name can't be  exceed 10 words")]
        [MinLength(2)]
        [MaxLength(10)]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Valid Email")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string? UserImage { get; set; }
        public byte[] HashedPassword { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<ReviewProcess>? ReviewProcesses{ get; set; } = new List<ReviewProcess>();
        public List<Wallpaper>? Wallpapers { get; set; } = new List<Wallpaper>();
    }
}
