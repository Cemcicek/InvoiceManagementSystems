namespace InvoiceManagementSystems.Core.DTOs
{
    public class BillDto
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? SequenceNo { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
