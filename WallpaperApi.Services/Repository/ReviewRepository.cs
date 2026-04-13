using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data;
using WallpaperApi.Data.Model;
using WallpaperApi.Services.Interface.Repository;

namespace WallpaperApi.Services.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReviewProcess> AddReviewAsync(ReviewProcess review)
        {
            _context.ReviewProcesses.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<ReviewProcess?> GetReviewByIdAsync(int id)
        {
            return await _context.ReviewProcesses
                .Include(r => r.Resolutions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ReviewProcess>> GetAllReviewsAsync()
        {
            return await _context.ReviewProcesses
                .Include(r => r.Resolutions)
                .ToListAsync();
        }

        public async Task<ReviewProcess?> UpdateReviewStatusAsync(int id, string action)
        {
            var review = await _context.ReviewProcesses
            .Include(r => r.Resolutions)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            if (action.ToLower() == "approve")
                review.Status = "Approved";
            else if (action.ToLower() == "reject")
                review.Status = "Rejected";
            else
                throw new ArgumentException("Invalid action. Use 'approve' or 'reject'.");

            await _context.SaveChangesAsync();
            return review;
        }

    }
}
