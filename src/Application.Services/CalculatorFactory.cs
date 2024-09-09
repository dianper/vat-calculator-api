namespace Application.Services
{
    using Application.Services.Exceptions;
    using Application.Services.Interfaces;
    using Application.Services.Requests;

    public sealed class CalculatorFactory : ICalculatorFactory
    {
        public IVATCalculator CreateCalculator(VATRequest request)
        {
            if (request.Net.HasValue)
            {
                return new NetCalculator();
            }
            else if (request.Gross.HasValue)
            {
                return new GrossCalculator();
            }
            else if (request.Vat.HasValue)
            {
                return new VATCalculator();
            }
            else
            {
                throw new InvalidCalculatorException("Invalid calculator request.");
            }
        }
    }
}
