namespace Application.Services.Requests
{
    using System.Text.Json.Serialization;

    public sealed class VATRequest
    {
        [JsonPropertyName("net")]
        public decimal? Net { get; set; }

        [JsonPropertyName("vat")]
        public decimal? Vat { get; set; }

        [JsonPropertyName("gross")]
        public decimal? Gross { get; set; }        

        [JsonPropertyName("vatRate")]
        public decimal VatRate { get; set; }
    }
}
