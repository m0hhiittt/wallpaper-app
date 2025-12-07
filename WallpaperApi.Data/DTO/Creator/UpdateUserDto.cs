using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WallpaperApi.DTO.Creator
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please upload an image file.")]
        public IFormFile File { get; set; }
    }
}
