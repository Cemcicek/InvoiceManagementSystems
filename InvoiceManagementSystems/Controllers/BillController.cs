using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        public IActionResult GetAllBills()
        {
            var bills = _billRepository.GetBills();
            return Ok(bills);
        }

        [HttpGet("unpaid")]
        public IActionResult GetUnpaidBills()
        {
            var bills = _billRepository.GetBills();
            var unpaidBills = bills.Where(b => b.Status == true).ToList(); ; // Ödenmemiş faturaları filtrele
            return Ok(unpaidBills);
        }

        [HttpGet("paid")]
        public IActionResult GetPaidBills()
        {
            var bills = _billRepository.GetBills();
            var paidBills = bills.Where(b => b.Status == false).ToList(); // Ödenmiş faturaları filtrele
            return Ok(paidBills);
        }
    }
}
