namespace Application.Services.Responses
{
    using System.Text.Json.Serialization;

    public sealed class VATResponse
    {
        [JsonPropertyName("net")]
        public double Net { get; set; }

        [JsonPropertyName("vat")]
        public double Vat { get; set; }

        [JsonPropertyName("gross")]
        public double Gross { get; set; }        

        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public VATResponse(double net, double gross, double vat)
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
