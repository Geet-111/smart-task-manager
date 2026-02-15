using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.API.Data;
using SmartTaskManager.API.DTOs;
using SmartTaskManager.API.Models;

namespace SmartTaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password, // temporarily plain text (we will hash later)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }
    }
}
