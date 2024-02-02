using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IBillTypeRepository
    {
        ICollection<BillType> GetBillTypes();
        BillType GetBillType(int id);
        bool BillTypeExists(int id);
        bool CreateBillType(BillType billType);
        bool UpdateBillType(BillType billType);
        bool DeleteBillType(BillType billType);
        bool Save();
    }
}
