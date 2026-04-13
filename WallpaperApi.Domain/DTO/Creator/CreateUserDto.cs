

using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Creator
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? LastName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? Email { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
