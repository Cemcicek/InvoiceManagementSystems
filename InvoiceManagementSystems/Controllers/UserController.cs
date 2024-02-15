using AutoMapper;
using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Core.Helper;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }

        [HttpGet]
        [Route("getuser")]
        public IActionResult GetUser()
        {
            var userMail = User.Identity?.Name;
            var userid = _userRepository.GetUserByMail(userMail);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(userid.Id);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [HttpPut]
        [Route("updateuser")]
        public IActionResult UpdateUser([FromBody] User updatedUser)
        {
            var userMail = User.Identity?.Name;
            var userId = _userRepository.GetUserByMail(userMail);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _userRepository.GetUserById(userId.Id);
            users.FirstName = updatedUser.FirstName;
            users.LastName = updatedUser.LastName;
            users.Email = updatedUser.Email;
            users.TC = updatedUser.TC;
            users.Tel = updatedUser.Tel;
            users.CarPlate = updatedUser.CarPlate;
            users.ApartmentOwner = updatedUser.ApartmentOwner;

            if (!_userRepository.UpdateUser(users))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        
        [HttpPut]
        [Route("updateuserpassword")]
        public IActionResult UpdateUserPassword([FromBody] UserPasswordDto userPasswordDto)
        {
            var userMail = User.Identity?.Name;
            var users = _userRepository.GetUserByMail(userMail);

            if (HashingHelper.VerifyHash(userPasswordDto.OldPassword, users.PasswordHash, users.PasswordSalt))
            {
                var userId = _userRepository.GetUserById(users.Id);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userPasswordDto.Password, out passwordHash, out passwordSalt);
                if (userPasswordDto.OldPassword != userPasswordDto.Password)
                {
                    userId.PasswordHash = passwordHash;
                    userId.PasswordSalt = passwordSalt;

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (!_userRepository.UpdateUser(users))
                    {
                        ModelState.AddModelError("", "Something went wrong updating user");
                        return StatusCode(500, ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şifrenizi mevcut şifreniz olarak güncelleyemezsiniz!");
                    return StatusCode(402, ModelState);
                }
            }
            return Ok();
        }
    }
}
