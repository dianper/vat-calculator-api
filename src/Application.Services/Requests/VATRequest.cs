namespace Application.Services.Requests
{
    using System.Text.Json.Serialization;

    public sealed class VATRequest
    {
        [JsonPropertyName("net")]
        public double? Net { get; set; }

        [JsonPropertyName("vat")]
        public double? Vat { get; set; }

        [JsonPropertyName("gross")]
        public double? Gross { get; set; }        

        [JsonPropertyName("vatRate")]
        public double VatRate { get; set; }
    }
}
