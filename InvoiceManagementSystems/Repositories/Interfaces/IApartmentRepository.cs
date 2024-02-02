using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Repositories.Interfaces
{
    public interface IApartmentRepository
    {
        ICollection<Apartment> GetApartments();
        ICollection<UserDto> GetUsers();
        User GetUser(int userId);
        IEnumerable<object> GetApartment(int id);
        Apartment GetApartmentById(int id);
        Apartment GetApartmentOfAUser(string email);
        bool ApartmentExists(int id);
        bool CreateApartment(Apartment apartment);
        bool UpdateApartment(Apartment apartment);
        bool DeleteApartment(Apartment apartment);
        bool Save();
    }
}
