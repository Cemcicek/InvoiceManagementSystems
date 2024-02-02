using AutoMapper;
using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        [HttpGet]
        [Route("getadmin")]
        public IActionResult GetAdmins()
        {
            var admins = _adminRepository.GetAdmins();
            return Ok(admins);
        }

        [HttpPut("{adminId}")]
        public IActionResult UpdateAdmin(int adminId, [FromBody] AdminDto updateAdmin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var existingAdmin = _adminRepository.GetAdmin(adminId);

            _mapper.Map(updateAdmin, existingAdmin);
            _adminRepository.UpdateAdmin(existingAdmin);

            return NoContent();
        }

        [HttpGet]
        [Route("getusers")]
        public IActionResult GetUsers()
        {
            var users = _adminRepository.GetUsers();
            return Ok(users);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User updateUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var users = _adminRepository.GetUserById(userId);
            users.FirstName = updateUser.FirstName;


            _adminRepository.UpdateUser(updateUser);

            return NoContent();
        }
    }
}
