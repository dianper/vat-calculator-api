﻿namespace Application.Services
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;

    public sealed class VATCalculator : IVATCalculator
    {
        public VATResponse Calculate(VATRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(request.Vat, nameof(request));

            var vat = request.Vat.Value;
            var net = Math.Round(vat / request.VatRate, 2);
            var gross = Math.Round(net + vat, 2);

            return new VATResponse(net, gross, vat);
        }
    }
}
