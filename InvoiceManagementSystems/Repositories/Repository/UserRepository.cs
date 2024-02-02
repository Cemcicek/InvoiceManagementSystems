using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public UserRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;

        }

        public User GetUser(int userId)
        {
            return _applicationDBContext.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        public User GetUserById(int id)
        {
            return _applicationDBContext.Users.Where(e => e.Id == id).FirstOrDefault();
        }

        public User GetUserByMail(string email)
        {
            return _applicationDBContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _applicationDBContext.Update(user);
            return Save();
        }
    }
}
