namespace CrossCutting.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class VATRate
    {
        public string Description { get; set; }

        public double Rate { get; set; }
    }
}
