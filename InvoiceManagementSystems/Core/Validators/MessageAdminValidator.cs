using FluentValidation;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Core.Validators
{
    public class MessageAdminValidator : AbstractValidator<MessageAdmin>
    {
        public MessageAdminValidator()
        {
            RuleFor(messageAdmin => messageAdmin.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz.");
            RuleFor(messageAdmin => messageAdmin.Comment).NotEmpty().WithMessage("Açıklama alanı boş olamaz.");
        }
    }
}
