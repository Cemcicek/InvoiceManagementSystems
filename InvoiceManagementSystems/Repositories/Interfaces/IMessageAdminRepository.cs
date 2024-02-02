using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IMessageAdminRepository
    {
        ICollection<MessageAdmin> GetMessageAdmins();
        ICollection<MessageAdmin> GetMessageAdmin(int id);
        User GetUserByMail(string email);
        bool MessageAdminExists(int id);
        bool CreateMessageAdmin(MessageAdmin messageAdmin);
        bool UpdateMessageAdmin(MessageAdmin messageAdmin);
        bool DeleteMessageAdmin(MessageAdmin messageAdmin);
        bool Save();
    }
}
