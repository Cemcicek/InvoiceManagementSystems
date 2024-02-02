using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        ICollection<Admin> GetAdmins();
        Admin GetAdmin(int id);
        Admin GetAdminByEmail(string email);
        ICollection<UserDto> GetUsers();
        User GetUserById(int id);
        bool UpdateUser(User user);
        bool AdminExists(int adminId);
        bool UpdateAdmin(Admin admin);
        bool Save();
    }
}
