namespace Application.Services.Responses
{
    using System.Text.Json.Serialization;

    public sealed class VATResponse
    {
        [JsonPropertyName("net")]
        public decimal Net { get; set; }

        [JsonPropertyName("vat")]
        public decimal Vat { get; set; }

        [JsonPropertyName("gross")]
        public decimal Gross { get; set; }        

        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public VATResponse(decimal net, decimal gross, decimal vat)
        {
            Net = net;
            Vat = vat;
            Gross = gross;
            IsValid = true;
            Message = "VAT calculation successful.";
        }

        public VATResponse(string errorMessage)
        {
            IsValid = false;
            Message = errorMessage;
        }

        public VATResponse()
        {

        }
    }
}
