namespace InvoiceManagementSystems.Core.DTOs
{
    public class UserRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? TC { get; set; }
        public string? Tel { get; set; }
        public string ApartmentOwner { get; set; }
        public DateTime Date { get; set; }
    }
}
