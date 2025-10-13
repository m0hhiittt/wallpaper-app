using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data.Model;

namespace WallpaperApi.Services.Interface.Repository
{
    public interface IReviewRepository
    {
        Task<ReviewProcess> AddReviewAsync(ReviewProcess review);
        Task<ReviewProcess?> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewProcess>> GetAllReviewsAsync();
        Task<ReviewProcess?> UpdateReviewStatusAsync(int id, string action);
    }
}
