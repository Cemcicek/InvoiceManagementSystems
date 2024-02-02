using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByMail(string email);
        User GetUser(int userId);
        User GetUserById(int id);
        bool UpdateUser(User user);
        bool Save();
    }
}
