namespace Application.Services
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;

    public sealed class VATCalculator : IVATCalculator
    {
        public VATResponse Calculate(VATRequest request)
        {
            double net, gross, vat;

            if (request.Net.HasValue)
            {
                net = request.Net.Value;
                vat = Math.Round(net * request.VatRate, 2);
                gross = Math.Round(net + vat, 2);
            }
            else if (request.Gross.HasValue)
            {
                gross = request.Gross.Value;
                vat = Math.Round(gross * request.VatRate / (1 + request.VatRate), 2);
                net = Math.Round(gross - vat, 2);
            }
            else
            {
                vat = request.Vat.Value;
                net = Math.Round(vat / request.VatRate, 2);
                gross = Math.Round(net + vat, 2);
            }

            return new VATResponse(net, gross, vat);
        }
    }
}
