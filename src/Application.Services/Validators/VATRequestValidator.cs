namespace Application.Services.Validators
{
    using Application.Services.Requests;
    using FluentValidation;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class VATRequestValidator : AbstractValidator<VATRequest>
    {
        public VATRequestValidator()
        {
            // At least one amount must be provided
            RuleFor(x => x)
                .Must(x => (x.Net.HasValue ? 1 : 0) +
                           (x.Gross.HasValue ? 1 : 0) +
                           (x.Vat.HasValue ? 1 : 0) == 1)
                .WithMessage("Exactly one of Net, Gross, or Vat amount must be provided.");

            // Each provided amount must be greater than zero
            When(x => x.Net.HasValue, () =>
            {
                RuleFor(x => x.Net!.Value)
                    .GreaterThan(0)
                    .WithMessage("Net amount must be a positive number.");
            });

            When(x => x.Gross.HasValue, () =>
            {
                RuleFor(x => x.Gross!.Value)
                    .GreaterThan(0)
                    .WithMessage("Gross amount must be a positive number.");
            });

            When(x => x.Vat.HasValue, () =>
            {
                RuleFor(x => x.Vat!.Value)
                    .GreaterThan(0)
                    .WithMessage("VAT amount must be a positive number.");
            });

            // VAT rate must be one of the valid rates
            RuleFor(x => x.VatRate)
                .Must(rate => rate == 0.10 || rate == 0.13 || rate == 0.20)
                .WithMessage("Invalid VAT rate. It must be 10%, 13%, or 20%.");
        }
    }
}
