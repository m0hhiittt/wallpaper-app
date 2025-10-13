using Microsoft.AspNetCore.Mvc;
using WallpaperApi.DTO.Creator;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi.Services.Services;

namespace WallpaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICreatorService _creatorService;
        private readonly IJwtService _jwtService;

        public AuthController(ICreatorService creatorService, IJwtService jwtService)
        {
            _creatorService = creatorService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var creator = await _creatorService.GetByEmailAndPasswordAsync(request.Email, request.Password);

            if (creator == null)
                return Unauthorized("Invalid email or password");

            var token = _jwtService.GenerateJwtToken(creator);

            // ✅ Optionally store in cookie
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(60)
            });

            return Ok(new
            {
                Token = token,
                User = creator
            });
        }
    }

    public record LoginRequest(string Email, string Password);
}
