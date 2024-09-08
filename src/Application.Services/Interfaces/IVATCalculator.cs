namespace Application.Services.Interfaces
{
    using Application.Services.Requests;
    using Application.Services.Responses;

    public interface IVATCalculator : ICalculator<VATRequest, VATResponse>
    {
    }
}
