using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Review.Services.Mapper;
using System.Text;
using WallpaperApi.Data;
using WallpaperApi.Data.DTO.AuthDto;
using WallpaperApi.DTO.Creator;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Mapper;
using WallpaperApi.Services.Interface.Service;

namespace WallpaperApi.Services
{
    public class AuthenticationService : IAuthenticatedService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreatorDto> IsUserAuthenticated(string username, string password)
        {
            var user = await _context.Creators.FirstOrDefaultAsync(u => u.Email == username);

            if (user == null)
            {
                return null;
            }
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (!user.HashedPassword.SequenceEqual(hashedPassword))
            {
                return null;
            }

            return new CreatorDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ReviewDtos = user.ReviewProcesses?.Select(ReviewProcessMapper.ToReviewDto).ToList(),
                Wallpapers = user.Wallpapers?.Select(WallpaperMapper.ToWallpaperDto).ToList()
            };
        }

    }
}
