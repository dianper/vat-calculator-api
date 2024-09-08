namespace Application.Services.Interfaces
{
    public interface ICalculator<in TRequest, out TResponse>
        where TRequest : class
        where TResponse : class
    {
        TResponse Calculate(TRequest request);
    }
}
