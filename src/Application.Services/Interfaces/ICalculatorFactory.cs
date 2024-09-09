namespace Application.Services.Interfaces
{
    using Application.Services.Requests;

    public interface ICalculatorFactory
    {
        IVATCalculator CreateCalculator(VATRequest request);
    }
}
