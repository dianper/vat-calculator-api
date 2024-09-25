namespace Application.Services
{
    using Application.Services.Exceptions;
    using Application.Services.Interfaces;
    using Application.Services.Requests;

    public sealed class CalculatorFactory : ICalculatorFactory
    {
        private readonly Dictionary<Func<VATRequest, bool>, Func<IVATCalculator>> calculatorMap;

        public CalculatorFactory()
        {
            this.calculatorMap = new Dictionary<Func<VATRequest, bool>, Func<IVATCalculator>>
            {
                { r => r.Net.HasValue, () => new NetCalculator() },
                { r => r.Gross.HasValue, () => new GrossCalculator() },
                { r => r.Vat.HasValue, () => new VATCalculator() }
            };
        }

        public IVATCalculator CreateCalculator(VATRequest request)
        {
            foreach (var pair in this.calculatorMap)
            {
                if (pair.Key(request))
                {
                    return pair.Value();
                }
            }

            throw new InvalidCalculatorException("Invalid calculator request.");
        }
    }
}
