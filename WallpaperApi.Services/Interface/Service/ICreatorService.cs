using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.DTO.Creator;

namespace WallpaperApi.Services.Interface.Service
{
    public interface ICreatorService
    {
        Task<List<CreatorDto>> GetAllAsync();
        Task<CreatorDto> GetByIdAsync(int id);
        Task<CreatorDto?> GetByEmailAndPasswordAsync(string email, string password);
        Task<CreatorDto> CreateAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
