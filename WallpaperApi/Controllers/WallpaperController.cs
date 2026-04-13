using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WallpaperApi.Data.DTO.Resolutions;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi.Services.Mapper;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class WallpaperController : Controller
    {
        private readonly IWallpaperService _wallpaperService;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WallpaperController(IWallpaperService wallpaperService, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _wallpaperService = wallpaperService;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWallpapers(int pageNumber = 1, int pageSize = 10)
        {
            var baseUrl = GetBaseUrl();
            var (wallpapers, totalCount) = await _wallpaperService.GetAllWallpapersService(pageNumber, pageSize);

            var result = wallpapers.Select(w => new WallpaperDto
            {
                Id = w.Id,
                Title = w.Title,
                WallpaperTag = w.WallpaperTag,
                FilePath = $"{baseUrl}/{w.FilePath}",
                Extention = w.Extention,

                Resolutions = w.Resolutions
                    .Select(r => new ResolutionDTO
                    {
                        Id = r.Id,
                        Resolution720P = $"{baseUrl}/{r.Resolution720P}",
                        Resolution1080P = $"{baseUrl}/{r.Resolution1080P}",
                        Resolution2K = $"{baseUrl}/{r.Resolution2K}",
                        Resolution4K = $"{baseUrl}/{r.Resolution4K}",
                        Thumbnail = $"{baseUrl}/{r.Thumbnail}",
                        WallpaperId = r.WallpaperId,
                        ReviewProcessId = r.ReviewProcessId,
                    })
                    .ToList()
            }).ToList();

            return Ok(new
            {
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = result
            });
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return $"{request?.Scheme}://{request?.Host}";
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Uploader([FromForm] CreateWallpaperDto wallpaperDto)
        {
            var response = await _wallpaperService.UploadWallpaperService(wallpaperDto);

            
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWallpaper(int id, [FromForm] UpdateWallpaperDto fileModel)
        {
            var response = await _wallpaperService.UpdateWallpaperService(id,fileModel);

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteWallpaper(int id)
        {
            var deleted = await _wallpaperService.DeleteWallpaperService(id);

            if (deleted == null)
                return NotFound(new { message = "Wallpaper not found" });

            return Ok(new { message = "Wallpaper deleted successfully" });
        }
    }
}
