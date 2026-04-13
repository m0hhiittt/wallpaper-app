using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime CreatedOn { get; set; }
    }
}
