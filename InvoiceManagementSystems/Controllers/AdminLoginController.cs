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
    public class AdminLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _applicationDBContext;
        public AdminLoginController(IConfiguration configuration, ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            _configuration = configuration;
        }

        [HttpPost("adminlogin")]
        public async Task<ActionResult> Login(AdminLoginDto adminLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = _applicationDBContext.Admins.SingleOrDefault(a => a.Email == adminLoginDto.Email);

            if (admin == null || !HashingHelper.VerifyHash(adminLoginDto.Password, admin.PasswordHash, admin.PasswordSalt))
            {
                return BadRequest("Hatalı Giriş");
            }

            string token = CreateToken(admin);
            return Ok(token);
        }

        private string CreateToken(Admin admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Email),
                new Claim(ClaimTypes.Role, "Admin")
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
