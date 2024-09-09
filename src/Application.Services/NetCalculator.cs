namespace Application.Services
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;

    public sealed class NetCalculator : IVATCalculator
    {
        public VATResponse Calculate(VATRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(request.Net, nameof(request));

            var net = request.Net.Value;
            var vat = Math.Round(net * request.VatRate, 2);
            var gross = Math.Round(net + vat, 2);

            return new VATResponse(net, gross, vat);
        }
    }
}
