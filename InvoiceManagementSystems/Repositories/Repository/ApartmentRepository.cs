using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Data;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;

namespace InvoiceManagementSystems.Repositories.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public ApartmentRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public bool ApartmentExists(int id)
        {
            return _applicationDBContext.Apartments.Any(c => c.Id == id);
        }

        public bool CreateApartment(Apartment apartment)
        {
            _applicationDBContext.Add(apartment);
            return Save();
        }

        public bool DeleteApartment(Apartment apartment)
        {
            _applicationDBContext.Remove(apartment);
            return Save();
        }

        public IEnumerable<object> GetApartment(int id)
        {
            var apartments = (
                from apartment in _applicationDBContext.Apartments
                join user in _applicationDBContext.Users on apartment.UserId equals user.Id
                where apartment.Id == id
                select new
                {
                    ApartmentId = apartment.Id,
                    ApartmentNo = apartment.ApartmentNo,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Tel = user.Tel
                }).ToList();

            return apartments;
        }

        public ICollection<Apartment> GetApartments()
        {
            return _applicationDBContext.Apartments.ToList();
            // var result = from apartment in _applicationDBContext.Apartments
            //     join user in _applicationDBContext.Users on apartment.UserId equals user.Id
            //     select new ApartmentWithUser
            //     {
            //         Id = apartment.Id,
            //         ApartmentNo = apartment.ApartmentNo,
            //         Floor = apartment.Floor,
            //         ApartmentBlock = apartment.ApartmentBlock,
            //         Type = apartment.Type,
            //         Status = apartment.Status,
            //         UserId = apartment.UserId,
            //         FirstName = user.FirstName,
            //         LastName = user.LastName,
            //         Email = user.Email
            //     };

            // return result.ToList();
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

        public User GetUser(int userId)
        {
            return _applicationDBContext.Users.Where(e => e.Id == userId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _applicationDBContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateApartment(Apartment apartment)
        {
            _applicationDBContext.Update(apartment);
            return Save();
        }

        public Apartment GetApartmentOfAUser(string email)
        {
            var userId = _applicationDBContext.Users.Where(x => x.Email == email).Select(y => y.Id).FirstOrDefault();
            return _applicationDBContext.Apartments.Where(c => c.UserId == userId).FirstOrDefault();
        }

        public Apartment GetApartmentById(int id)
        {
            return _applicationDBContext.Apartments.Where(e => e.Id == id).FirstOrDefault();
        }
    }
}
