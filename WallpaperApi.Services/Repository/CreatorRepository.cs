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

        public async Task<Creator?> GetByEmailAndPasswordAsync(string email, string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedPassword = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            );

            // Compare with stored hashed password
            return await _context.Creators
                .FirstOrDefaultAsync(c => c.Email == email && Convert.ToBase64String(c.HashedPassword) == hashedPassword);
        }

        public async Task<IEnumerable<Creator>> GetUserAsync()
        {
            return await _context.Creators.Include(x=>x.ReviewProcesses).Include(y=>y.Wallpapers).ToListAsync();
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
            UserExist.Email = creator.Email;

            await _context.SaveChangesAsync();

            return UserExist;
        }
    }
}
