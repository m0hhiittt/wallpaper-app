using WallpaperApi.DTO.Creator;
using WallpaperApi.Mapper;
using WallpaperApi.Services.Interface;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;

namespace WallpaperApi.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ICreatorRepository _repo;

        public CreatorService(ICreatorRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreatorDto> CreateAsync(CreateUserDto dto)
        {
            var user = CreatorMapper.ToCreateUserRequest(dto);
            var created = await _repo.CreateUserAsync(user);
            return CreatorMapper.ToCreatorDto(created);
        }
        public async Task<List<CreatorDto>> GetAllAsync()
        {
            var users = await _repo.GetUserAsync();
            return users.Select(CreatorMapper.ToCreatorDto).ToList();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null) return false;

            await _repo.DeleteUserAsync(id);
            return true;
        }

        public async Task<CreatorDto> GetByIdAsync(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            return user == null ? null : CreatorMapper.ToCreatorDto(user);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null) return false;

            CreatorMapper.ToCreatorUpdateRequest(dto);
            await _repo.UpdateUserAsync(dto, id);
            return true;
        }

        public async Task<CreatorDto?> GetByEmailAndPasswordAsync(string email, string password)
        {
            var creator = await _repo.GetByEmailAndPasswordAsync(email, password);
            return creator?.ToCreatorDto();
        }
    }
}
