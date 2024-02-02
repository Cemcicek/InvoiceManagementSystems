namespace InvoiceManagementSystems.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string? ApartmentNo { get; set; }
        public string? Floor { get; set; }
        public string? ApartmentBlock { get; set; }
        public string? Type { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
    }
}
