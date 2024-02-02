using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Core.Helper;
using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _applicationDBContext;
        public UserLoginController(IConfiguration configuration, ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            _configuration = configuration;

        }

        [HttpPost("userlogin")]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _applicationDBContext.Users.SingleOrDefault(a => a.Email == userLoginDto.Email);

            if (user == null || !HashingHelper.VerifyHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Hatalı Giriş");
            }

            if (!user.Status)
            {
                return BadRequest("Hesabınız aktif değil. Lütfen yöneticiyle iletişime geçin.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost("userregister")]
        public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_applicationDBContext.Users.Any(u => u.Email == userRegisterDto.Email))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmaktadır.");
                return BadRequest(ModelState);
            }

            // kullanıcı hesabına giriş yapabilmesi admin kullanıcısının doğrulamasını istiyoruz
            // Status = false admin onaylayacak => Status = true
            var user = new User
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                TC = userRegisterDto.TC,
                Tel = userRegisterDto.Tel,
                ApartmentOwner = userRegisterDto.ApartmentOwner,
                Status = false,
                Date = DateTime.Now
            };

            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _applicationDBContext.Add(user);

            try
            {
                var saved = await _applicationDBContext.SaveChangesAsync();
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
