using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class BillTypeController : ControllerBase
    {
        private readonly IBillTypeRepository _billTypeRepository;
        public BillTypeController(IBillTypeRepository billTypeRepository)
        {
            _billTypeRepository = billTypeRepository;

        }

        [HttpGet]
        public IActionResult GetBillTypes()
        {
            var billType = _billTypeRepository.GetBillTypes();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(billType);
        }

        [HttpGet("{billTypeId}")]
        public IActionResult GetBillType(int billTypeId)
        {
            if (!_billTypeRepository.BillTypeExists(billTypeId))
                return NotFound();

            var billType = _billTypeRepository.GetBillType(billTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(billType);
        }

        [HttpPost]
        public IActionResult CreateBillType([FromBody] BillType billType)
        {
            var created = _billTypeRepository.GetBillTypes().Where(c => c.BillTypeName.Trim().ToUpper() == billType.BillTypeName.TrimEnd().ToUpper()).FirstOrDefault();
            if (created != null)
            {
                ModelState.AddModelError("", "Fatura türü zaten mevcut!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var billTypeCreate = _billTypeRepository.CreateBillType(billType);
            return Ok("Kayıt eklendi!");
        }

        [HttpPut("{billTypeId}")]
        public IActionResult UpdateBillType(int billTypeId, [FromBody] BillType billType)
        {

            if (billType == null)
                return BadRequest(ModelState);

            if (billTypeId != billType.Id)
                return BadRequest(ModelState);

            if (!_billTypeRepository.BillTypeExists(billTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_billTypeRepository.UpdateBillType(billType))
            {
                ModelState.AddModelError("", "Bir hata oluştu!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{billTypeId}")]
        public IActionResult DeleteBillType(int billTypeId)
        {
            if (!_billTypeRepository.BillTypeExists(billTypeId))
            {
                return NotFound();
            }

            var billTypeDelete = _billTypeRepository.GetBillType(billTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_billTypeRepository.DeleteBillType(billTypeDelete))
            {
                ModelState.AddModelError("", "Bir hata oluştu!");
            }

            return NoContent();
        }
    }
}
