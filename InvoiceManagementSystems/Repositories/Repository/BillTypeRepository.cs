using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class BillTypeRepository : IBillTypeRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public BillTypeRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;

        }
        public bool BillTypeExists(int id)
        {
            return _applicationDBContext.BillTypes.Any(c => c.Id == id);
        }

        public bool CreateBillType(BillType billType)
        {
            _applicationDBContext.Add(billType);
            return Save();
        }

        public bool DeleteBillType(BillType billType)
        {
            _applicationDBContext.Remove(billType);
            return Save();
        }

        public BillType GetBillType(int id)
        {
            return _applicationDBContext.BillTypes.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<BillType> GetBillTypes()
        {
            return _applicationDBContext.BillTypes.ToList();
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBillType(BillType billType)
        {
            _applicationDBContext.Update(billType);
            return Save();
        }
    }
}
