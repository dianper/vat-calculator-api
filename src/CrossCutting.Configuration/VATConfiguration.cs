namespace CrossCutting.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class VATConfiguration
    {
        public IEnumerable<VATRate> Rates { get; set; }
    }
}
