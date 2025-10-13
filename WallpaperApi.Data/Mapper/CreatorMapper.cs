using Review.Services.Mapper;
using System.Text;
using WallpaperApi.DTO.Creator;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Mapper
{
    public static class CreatorMapper
    {
        public static CreatorDto ToCreatorDto(this Creator model)
        {
            return new CreatorDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = Convert.ToBase64String(model.HashedPassword),
                Email = model.Email,
                CreatedOn = model.CreatedOn,
                ReviewDtos = model.ReviewProcesses.Select(ReviewProcessMapper.ToReviewDto).ToList(),
                Wallpapers = model.Wallpapers.Select(WallpaperMapper.ToWallpaperDto).ToList(),
            };
        }

        public static Creator ToCreateUserRequest(this CreateUserDto creatorModel)
        {
            return new Creator
            {
                FirstName = creatorModel.FirstName,
                LastName = creatorModel.LastName,
                Email = creatorModel.Email,
                HashedPassword = HashPassword(creatorModel.Password),
                CreatedOn = creatorModel.CreatedOn
            };
        }

        public static Creator ToCreatorUpdateRequest(this UpdateUserDto creatorModel)
        {
            return new Creator
            {
                FirstName = creatorModel.FirstName,
                LastName = creatorModel.LastName,
                Email = creatorModel.Email,
            };
        }

        private static byte[] HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
