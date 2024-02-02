using FluentValidation;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Core.Validators
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            RuleFor(bill => bill.SerialNumber).NotEmpty().WithMessage("Fatura seri numarası boş olamaz.");
            RuleFor(bill => bill.Price).NotEmpty().WithMessage("Fatura fiyatı boş olamaz.");
            RuleFor(bill => bill.Price).Must(BeValidDecimal).WithMessage("Fatura fiyatı geçerli bir sayı olmalıdır.");
            RuleFor(bill => bill.Date).NotEmpty().WithMessage("Fatura tarihi boş olamaz.");
        }
        private bool BeValidDecimal(string value)
        {
            return decimal.TryParse(value, out _);
        }
    }
}
