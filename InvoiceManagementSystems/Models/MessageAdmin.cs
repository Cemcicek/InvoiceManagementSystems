namespace InvoiceManagementSystems.Models
{
    public class MessageAdmin
    {
        public int Id { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
