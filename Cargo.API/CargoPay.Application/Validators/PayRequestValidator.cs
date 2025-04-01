using CargoPay.Application.DTOS;
using FluentValidation;

namespace CargoPay.Application.Validators
{
    public class PayRequestValidator : AbstractValidator<PayRequest>
    {
        public PayRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().Length(15).WithMessage("Card number must be 15 digits");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero");
        }
    }

}
