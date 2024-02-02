using Microsoft.VisualBasic;

namespace InvoiceManagementSystems.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? SequenceNo { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int BillTypeId { get; set; }
        public BillType BillType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
