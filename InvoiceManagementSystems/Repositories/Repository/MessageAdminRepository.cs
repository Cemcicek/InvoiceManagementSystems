using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class MessageAdminRepository : IMessageAdminRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public MessageAdminRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;

        }
        public bool CreateMessageAdmin(MessageAdmin messageAdmin)
        {
            _applicationDBContext.Add(messageAdmin);
            return Save();
        }

        public bool DeleteMessageAdmin(MessageAdmin messageAdmin)
        {
            _applicationDBContext.Remove(messageAdmin);
            return Save();
        }

        public ICollection<MessageAdmin> GetMessageAdmin(int userId)
        {
            return _applicationDBContext.MessageAdmins.Where(m => m.UserId == userId).ToList();
        }

        public ICollection<MessageAdmin> GetMessageAdmins()
        {
            return _applicationDBContext.MessageAdmins.ToList();
        }

        public User GetUserByMail(string email)
        {
            return _applicationDBContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public bool MessageAdminExists(int id)
        {
            return _applicationDBContext.MessageAdmins.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMessageAdmin(MessageAdmin messageAdmin)
        {
            _applicationDBContext.Update(messageAdmin);
            return Save();
        }
    }
}
