using Microsoft.AspNetCore.Mvc;
using WallpaperApi.DTO.Creator;
using WallpaperApi.Mapper;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;

namespace WallpaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController : Controller
    {
        private readonly ICreatorService _service;

        public CreatorController(ICreatorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetCreatorById(int id)
        {
            var exist = await _service.GetByIdAsync(id);
            return exist == null ? NotFound() : Ok(exist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto creator)
        {
            var created = await _service.CreateAsync(creator);
            return CreatedAtAction(nameof(GetCreatorById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallpaper([FromBody]UpdateUserDto userDto ,int id)
        {
            var exist = await _service.GetByIdAsync(id);
            if (exist == null)
                return NotFound();

            var update = await _service.UpdateAsync(id, userDto);
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallpaper(int id)
        {
            var userExist = await _service.GetByIdAsync(id);

            if (userExist == null)
            {
                return NotFound("creator not Found");
            }

            await _service.DeleteAsync(id);

            return Ok(userExist);
        }
    }
}
