using WallpaperApi.DTO.Creator;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services.Interface.Repository
{
    public interface ICreatorRepository
    {
        Task<IEnumerable<Creator>> GetUserAsync();
        Task<Creator> GetUserByIdAsync(int id);
        Task<Creator> CreateUserAsync(Creator creator);
        Task<Creator> UpdateUserAsync(UpdateUserDto creator, int id);
        Task<Creator> DeleteUserAsync(int id);
    }
}
