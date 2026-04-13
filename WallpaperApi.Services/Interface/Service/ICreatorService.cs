using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data.DTO.Creator;
using WallpaperApi.DTO.Creator;

namespace WallpaperApi.Services.Interface.Service
{
    public interface ICreatorService
    {
        Task<List<CreatorDto>> GetAllAsync();
        Task<CreatorDto> GetByIdAsync(int id);
        Task<CreatorDto> CreateAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(UpdateUserDto dto, int id);
        Task<bool> DeleteAsync(int id);
    }
}
