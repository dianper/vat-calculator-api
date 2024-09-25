namespace CrossCutting.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class VATRate
    {
        public string Description { get; set; }

        public decimal Rate { get; set; }
    }
}
