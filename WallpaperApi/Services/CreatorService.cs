using WallpaperApi.Data.DTO.Creator;
using WallpaperApi.DTO.Creator;
using WallpaperApi.Mapper;
using WallpaperApi.Services.Interface;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ICreatorRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CreatorService(ICreatorRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
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

        public async Task<bool> UpdateAsync(UpdateUserDto dto, int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null)
                return false;

            var roothPath = _env.ContentRootPath;
            var path = Path.Combine(roothPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            CreatorMapper.ToCreatorUpdateRequest(dto);
            await _repo.UpdateUserAsync(dto, id);
            return true;
        }
    }
}
