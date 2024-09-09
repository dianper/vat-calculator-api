namespace Application.Services
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;

    public sealed class GrossCalculator : IVATCalculator
    {
        public VATResponse Calculate(VATRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(request.Gross, nameof(request));

            var gross = request.Gross.Value;
            var vat = Math.Round(gross * request.VatRate / (1 + request.VatRate), 2);
            var net = Math.Round(gross - vat, 2);

            return new VATResponse(net, gross, vat);
        }
    }
}
