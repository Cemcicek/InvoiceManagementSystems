using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;
        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        /// <summary>
        /// Uygulamadaki daire verilerini getirmek için kullanılır.
        /// Yalnızca "Admin" rolüne sahip kullanıcılara yetki verilmiştir.
        /// </summary>

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetApartments()
        {
            var apartment = _apartmentRepository.GetApartments();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(apartment);
        }


        /// <summary>
        /// Belirli bir apartmentId'ye sahip olan apartmanı ve ait olduğu kullanıcının özelliklerini listeler.
        /// </summary>
        /// <param name="apartmentId">Listelenecek apartmanın ID'si.</param>
        /// <returns>Apartman ve kullanıcı bilgilerini içeren liste.</returns>

        [HttpGet("{apartmentId}")]
        public IActionResult GetApartment(int apartmentId)
        {
            if (!_apartmentRepository.ApartmentExists(apartmentId))
                return NotFound();

            var apartment = _apartmentRepository.GetApartment(apartmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(apartment);
        }

        /// <summary>
        /// Belirli bir apartmanın bilgilerini güncelleyen yöntem.
        /// Sadece "Admin" rolüne sahip kullanıcılar yetkilidir.
        /// </summary>
        /// <param name="apartmentId">Güncellenecek apartmanın ID'si.</param>
        /// <param name="updatedApartment">Güncellenmiş apartman bilgilerini içeren nesne.</param>

        [HttpPut("{apartmentId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateApartment(int apartmentId, [FromBody] Apartment updatedApartment)
        {
            if (updatedApartment == null)
                return BadRequest(ModelState);
            if (apartmentId != updatedApartment.Id)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();

            var apartment = _apartmentRepository.GetApartmentById(apartmentId);
            apartment.ApartmentNo = updatedApartment.ApartmentNo;
            apartment.Floor = updatedApartment.Floor;
            apartment.ApartmentBlock = updatedApartment.ApartmentBlock;
            apartment.Type = updatedApartment.Type;
            apartment.Status = updatedApartment.Status;

            if (!_apartmentRepository.UpdateApartment(apartment))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Tüm kullanıcıları getiren yöntem.
        /// Sadece "Admin" rolüne sahip kullanıcılar yetkilidir.
        /// </summary>
        /// <returns>Veritabanındaki tüm kullanıcıları içeren bir cevap.</returns>

        [HttpGet]
        [Route("getusers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var apartment = _apartmentRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(apartment);
        }

        /// <summary>
        /// Belirli bir kullanıcıya ait yeni bir apartman oluşturur.
        /// </summary>
        /// <param name="userId">Apartmanın oluşturulacağı kullanıcının ID'si.</param>
        /// <param name="apartment">Oluşturulacak apartmanın bilgileri.</param>
        /// <returns>Oluşturulan apartmanın başarıyla oluşturulduğunu belirten mesaj.</returns>

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateApartment(int userId, [FromBody] Apartment apartment)
        {
            if (apartment == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _apartmentRepository.GetUser(userId);
            if (users != null)
            {
                var newApartment = new Apartment
                {
                    ApartmentNo = apartment.ApartmentNo,
                    Floor = apartment.Floor,
                    ApartmentBlock = apartment.ApartmentBlock,
                    Type = apartment.Type,
                    Status = apartment.Status,
                    UserId = users.Id
                };
                var billTypeCreate = _apartmentRepository.CreateApartment(newApartment);
            }
            else
            {
                ModelState.AddModelError("", "Kayıtlı kullanıcı bulunamadı!");
            }
            return Ok("Successfully created");
        }


        /// <summary>
        /// Kullanıcının apartmanını getiren yöntem.
        /// Yalnızca "User" rolüne sahip kullanıcılar yetkilidir.
        /// </summary>
        /// <returns>
        /// Kullanıcının e-posta adresini, e-posta adresine karşılık gelen kullanıcı ID'sini 
        /// kullanarak bulur ve bu ID'ye sahip apartman bilgisini içeren bir cevabı döndürür.
        /// </returns>

        [HttpGet]
        [Route("getapartmentofuser")]
        [Authorize(Roles = "User")]
        public IActionResult GetApartmentOfAUser()
        {
            var userMail = User.Identity?.Name;
            var userId = _apartmentRepository.GetApartmentOfAUser(userMail);
            return Ok(userId);
        }
    }
}
