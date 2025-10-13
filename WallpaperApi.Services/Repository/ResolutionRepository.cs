using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data;
using WallpaperApi.Data.Model;

namespace WallpaperApi.Services.Repository
{
    public interface IResolutionRepository
    {
        Task<Resolutions> AddResolutionsAsync(Resolutions resolutions);
        Task<Resolutions> GetResolutionsByIdAsync(int id);
        Task<bool> UpdateResolutionsAsync(Resolutions resolutions, int id);
        Task<bool> DeleteResolutionsAsync(int id);
        Task<IEnumerable<Resolutions>> GetAllResolutions();
    }

    public class ResolutionRepository:IResolutionRepository
    {
        private readonly ApplicationDbContext _context;

        public ResolutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Resolutions> AddResolutionsAsync(Resolutions resolutions)
        {
            _context.Resolutions.Add(resolutions);
            await _context.SaveChangesAsync();
            return resolutions;
        }

        public async Task<bool> DeleteResolutionsAsync(int id)
        {
            var find = await _context.Resolutions.FindAsync(id);
            if(find == null)
                return false;

            _context.Resolutions.Remove(find);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Resolutions>> GetAllResolutions()
        {
            return await _context.Resolutions.ToListAsync();
        }

        public async Task<Resolutions> GetResolutionsByIdAsync(int id)
        {
            var find = await _context.Resolutions.FindAsync(id);
            return find == null ? null : find;
        }

        public async Task<bool> UpdateResolutionsAsync(Resolutions resolutions, int id)
        {
            var find = await _context.Resolutions.FindAsync(id);
            if (find == null)
                return false;

            _context.Resolutions.Update(find);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
