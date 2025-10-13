using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;

namespace WallpaperApi.Services.Interface.Service
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto);
        Task<ReviewDto?> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto?> UpdateReviewStatusAsync(int id, string action);
    }
}