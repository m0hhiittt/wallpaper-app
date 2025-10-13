
using System.ComponentModel.DataAnnotations;
using WallpaperApi.Model;

namespace WallpaperApi.DTO.Creator
{
    public class CreatorDto
    {
        [Required]
        public int Id { get; set; }
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
