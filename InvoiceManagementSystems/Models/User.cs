namespace InvoiceManagementSystems.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? TC { get; set; }
        public string? Tel { get; set; }
        public string? Image { get; set; }
        public string? CarPlate { get; set; }
        public string? ApartmentOwner { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}
