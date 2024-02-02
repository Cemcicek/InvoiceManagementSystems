using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IBillRepository
    {
        ICollection<BillDto> GetBills();
        Bill GetBill(int id);
        bool BillExists(int id);
        bool CreateBill(Bill bill);
        bool UpdateBill(Bill bill);
        bool DeleteBill(Bill bill);
        bool Save();
    }
}
