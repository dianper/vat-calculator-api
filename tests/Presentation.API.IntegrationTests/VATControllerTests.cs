namespace Presentation.API.IntegrationTests
{
    using Application.Services.Responses;
    using Microsoft.AspNetCore.Mvc.Testing;
    using System.Net;
    using System.Net.Http.Json;
    using Xunit;

    [Trait("Category", "Presentation.API.Controllers.VATController.IntegrationTests")]
    public sealed class VATControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public VATControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task CalculateVAT_WhenRequestIsValid_ShouldReturnOk()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/v1.0/vat", new
            {
                net = 100,
                vatRate = 0.20
            });

            // Assert
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<VATResponse>();

            Assert.NotNull(result);
            Assert.Equal(100, result.Net);
            Assert.Equal(20, result.Vat);
            Assert.Equal(120, result.Gross);
            Assert.True(result.IsValid);
            Assert.Equal("VAT calculation successful.", result.Message);
        }

        [Fact]
        public async Task CalculateVAT_WhenVATRateIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/v1.0/vat", new
            {
                net = 100,
                vatRate = 0.21
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<VATResponse>();

            Assert.NotNull(result);
            Assert.Equal("Invalid VAT rate. It must be 10%, 13%, or 20%.", result.Message);
        }

        [Fact]
        public async Task CalculateVAT_WhenMoreThanOneAmountIsProvided_ShouldReturnBadRequest()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/v1.0/vat", new
            {
                net = 100,
                vat = 20,
                vatRate = 0.20
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<VATResponse>();

            Assert.NotNull(result);
            Assert.Equal("Exactly one of Net, Gross, or Vat amount must be provided.", result.Message);
        }
    }
}