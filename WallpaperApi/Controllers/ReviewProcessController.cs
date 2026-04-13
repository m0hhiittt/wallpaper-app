using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Interface.Service;

namespace WallpaperApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewProcessController : Controller
    {
        private readonly IReviewService _service;

        public ReviewProcessController(IReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateReviewDto dto)
        {
            var result = await _service.CreateReviewAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _service.GetReviewByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _service.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateReviewDto dto, int id)
        {
            var updated = await _service.UpdateReviewStatusAsync(id, dto.Action);
            if (updated == null)
                return NotFound("Review not found");

            return Ok(new { updated.Id, updated.Status });
        }

    }
}