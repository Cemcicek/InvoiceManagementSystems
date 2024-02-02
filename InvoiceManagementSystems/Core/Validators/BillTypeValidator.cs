using FluentValidation;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Core.Validators
{
    public class BillTypeValidator : AbstractValidator<BillType>
    {
        public BillTypeValidator() 
        {
            RuleFor(b => b.BillTypeName).NotNull().NotEmpty().WithMessage("Boş Olamaz!");
        }
    }
}
