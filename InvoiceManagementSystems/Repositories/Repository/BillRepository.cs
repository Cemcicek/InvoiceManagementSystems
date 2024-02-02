using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public BillRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;

        }
        public bool BillExists(int id)
        {
            return _applicationDBContext.Bills.Any(c => c.Id == id);
        }

        public bool CreateBill(Bill bill)
        {
            _applicationDBContext.Add(bill);
            return Save();
        }

        public bool DeleteBill(Bill bill)
        {
            _applicationDBContext.Remove(bill);
            return Save();
        }

        public Bill GetBill(int id)
        {
            return _applicationDBContext.Bills.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<BillDto> GetBills()
        {
            // return _applicationDBContext.Bills
            // .Include(b => b.BillType)
            // .Include(b => b.User)     
            // .ToList();

            var result = from bill in _applicationDBContext.Bills
                         join user in _applicationDBContext.Users on bill.UserId equals user.Id
                         join billType in _applicationDBContext.BillTypes on bill.BillTypeId equals billType.Id
                         select new BillDto
                         {
                             Id = bill.Id,
                             SerialNumber = bill.SerialNumber,
                             SequenceNo = bill.SequenceNo,
                             Price = bill.Price,
                             Date = bill.Date,
                             Description = bill.Description,
                             Status = bill.Status,
                             BillTypeId = bill.BillTypeId,
                             BillTypeName = billType.BillTypeName,
                             UserId = user.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email
                         };

            return result.ToList();
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBill(Bill bill)
        {
            _applicationDBContext.Update(bill);
            return Save();
        }
    }
}
