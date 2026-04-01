using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotivAi.Models;
using MotivAi.Services;

namespace MotivAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MotivAiContext _context;
        private readonly AuthService _authService;

        public AuthController(MotivAiContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // تسجيل يوزر جديد
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // هل الإيميل موجود؟
            var exists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (exists)
                return BadRequest(new { message = "الإيميل ده موجود بالفعل" });

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password_hash = _authService.HashPassword(request.Password),
                Role_id = 1,
                Created_at = DateTime.UtcNow,
                Is_active = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateToken(user);
            return Ok(new { token, name = user.Name, email = user.Email });
        }

        // تسجيل الدخول
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var hashedPassword = _authService.HashPassword(request.Password);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email
                                       && u.Password_hash == hashedPassword);

            if (user == null)
                return Unauthorized(new { message = "الإيميل أو الباسورد غلط" });

            user.Last_login = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = _authService.GenerateToken(user);
            return Ok(new { token, name = user.Name, email = user.Email });
        }
    }

    public class RegisterRequest
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
