using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WallpaperApi.Data.DTO.AuthDto;
using WallpaperApi.DTO.Creator;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi.Services.Services;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticatedService _service;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthenticatedService service, IJwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto request)
        {
            var authUser = await _service.IsUserAuthenticated(request.Email, request.Password);

            if (authUser == null)
                return Unauthorized("Invalid email or password");

            var token = _jwtService.GenerateJwtToken(authUser);

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
                User = authUser,
            });
        }
    }   
}
