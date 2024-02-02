using FluentValidation;
using InvoiceManagementSystems.Core.DTOs;
using System.Text.RegularExpressions;

namespace InvoiceManagementSystems.Core.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
            .Must(BeAValidEmail).WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(user => user.TC)
            .NotEmpty().WithMessage("TC kimlik numarası boş olamaz")
            .Length(11).WithMessage("TC kimlik numarası 11 karakter olmalıdır")
            .Must(BeAValidTcIdentity).WithMessage("Geçerli bir TC kimlik numarası giriniz");

            RuleFor(user => user.Tel).NotEmpty().WithMessage("Telefon numarası boş olamaz")
            .Matches("^[0-9]").WithMessage("Telefon numarası sayı ile başlamalıdır");

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Ad boş olamaz");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Soyad boş olamaz");
            RuleFor(user => user.Password).NotEmpty().MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır");
            RuleFor(user => user.ApartmentOwner).NotNull().WithMessage("Boş olamaz");
        }

        private bool BeAValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool BeAValidTcIdentity(string tc)
        {
            if (tc.Length != 11)
                return false;

            // TC kimlik numarası algoritması
            int[] digits = tc.Select(c => int.Parse(c.ToString())).ToArray();
            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int tenthDigit = (oddSum * 7 - evenSum) % 10;
            if (digits[9] != tenthDigit)
                return false;

            int totalSum = oddSum + evenSum + digits[9];
            int eleventhDigit = totalSum % 10;
            if (digits[10] != eleventhDigit)
                return false;

            return true;
        }
    }
}
