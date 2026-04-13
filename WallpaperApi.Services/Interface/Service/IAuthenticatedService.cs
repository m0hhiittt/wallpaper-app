using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data.DTO.AuthDto;
using WallpaperApi.DTO.Creator;
using WallpaperApi.DTO.Wallpaper;

namespace WallpaperApi.Services.Interface.Service
{
    public interface IAuthenticatedService
    {
        Task<CreatorDto> IsUserAuthenticated(string username, string password);
    }
}
