using FluentValidation;
using InvoiceManagementSystems.Core.DTOs;

namespace InvoiceManagementSystems.Core.Validators
{
    public class UserPasswordDtoValidator : AbstractValidator<UserPasswordDto>
    {
        public UserPasswordDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.")
                .MinimumLength(6).WithMessage("Şifrenizin uzunluğu 6 karakterden büyük olmalıdır!");
        }
    }
}
