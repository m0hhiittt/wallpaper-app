using Microsoft.EntityFrameworkCore;
using System.Text;
using WallpaperApi.Data;
using WallpaperApi.DTO.Creator;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Repository
{
    public class CreatorRepository : ICreatorRepository
    {
        private readonly ApplicationDbContext _context;

        public CreatorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Creator> CreateUserAsync(Creator creator)
        {
            await _context.Creators.AddAsync(creator);
            await _context.SaveChangesAsync();
            return creator;
        }

        public async Task<Creator> DeleteUserAsync(int id)
        {
            var userExist = await _context.Creators.FirstOrDefaultAsync(x=>x.Id==id);

            if (userExist == null)
            {
                return null;
            }

            _context.Creators.Remove(userExist);
            await _context.SaveChangesAsync();

            return userExist;
        }

        public async Task<IEnumerable<Creator>> GetUserAsync()
        {
            return await _context.Creators.Include(x=>x.ReviewProcesses)
                .ThenInclude(r=>r.Resolutions)
                .Include(y=>y.Wallpapers)
                .ThenInclude(r=>r.Resolutions)
                .ToListAsync();
        }

        public async Task<Creator> GetUserByIdAsync(int id)
        {
            var UserExist = await _context.Creators.FirstOrDefaultAsync(x=>x.Id==id);

            if(UserExist == null)
            {
                return null;
            }

            return UserExist;
        }

        public async Task<Creator> UpdateUserAsync(UpdateUserDto creator, int id)
        {
            var UserExist = await _context.Creators.FirstOrDefaultAsync(x => x.Id == id);

            if (UserExist == null)
            {
                return null;
            }

            UserExist.FirstName = creator.FirstName;
            UserExist.LastName = creator.LastName;

            await _context.SaveChangesAsync();

            return UserExist;
        }
    }
}
