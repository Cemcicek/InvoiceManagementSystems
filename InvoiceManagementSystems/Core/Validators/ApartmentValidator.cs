using FluentValidation;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Core.Validators
{
    public class ApartmentValidator : AbstractValidator<Apartment>
    {
        public ApartmentValidator()
        {
            RuleFor(b => b.ApartmentNo).NotNull().WithMessage("Apartman numaranız boş olamaz!");
            RuleFor(b => b.ApartmentBlock).NotNull().WithMessage("Apartman bloğunuz boş olamaz!");
        }
    }
}
