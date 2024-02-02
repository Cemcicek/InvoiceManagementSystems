using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public AdminRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public bool AdminExists(int adminId)
        {
            return _applicationDBContext.Admins.Any(c => c.Id == adminId);
        }

        public Admin GetAdmin(int id)
        {
            return _applicationDBContext.Admins.Where(c => c.Id == id).FirstOrDefault();
        }

        public Admin GetAdminByEmail(string email)
        {
            return _applicationDBContext.Admins.FirstOrDefault(x => x.Email == email);
        }

        public ICollection<Admin> GetAdmins()
        {
            return _applicationDBContext.Admins.ToList();
        }

        public User GetUserById(int id)
        {
            return _applicationDBContext.Users.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<UserDto> GetUsers()
        {
            var users = _applicationDBContext.Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Tel = u.Tel,
                CarPlate = u.CarPlate,
                ApartmentOwner = u.ApartmentOwner,
                Status = u.Status,
                Date = u.Date
            }).ToList();

            return users;
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAdmin(Admin admin)
        {
            _applicationDBContext.Update(admin);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _applicationDBContext.Update(user);
            return Save();
        }
    }
}
