using FluentValidation;
using Receipt.Core.Models;

namespace Receipt.Core.Validations
{
    public class ItemValidator : AbstractValidator<Items>
    {
        public ItemValidator()
        {
            RuleFor(item => item.ProductName).NotEmpty().NotNull();
        }
    }
}