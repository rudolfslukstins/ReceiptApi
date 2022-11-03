using FluentValidation;
using Receipt.Core.Models;

namespace Receipt.Core.Validations
{
    public class ReceiptValidator : AbstractValidator<Receipts>
    {
        public ReceiptValidator()
        {
            RuleFor(receipt => receipt.Id).NotEmpty().NotNull();
            RuleFor(receipt => receipt.CreatedOn).NotEmpty().NotNull();
            RuleFor(receipt => receipt.ItemsList).NotEmpty().NotNull();
        }
        
    }
}