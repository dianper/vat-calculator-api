namespace Application.Services.UnitTests
{
    using Application.Services.Requests;
    using Xunit;

    [Trait("Category", "Application.Services.VATCalculator.UnitTests")]
    public sealed class VATCalculatorTests
    {
        [Fact]
        public void Calculate_WhenNetIsProvided_ShouldReturnCorrectValues()
        {
            // Arrange
            var request = new VATRequest
            {
                Net = 100,
                VatRate = 0.23
            };

            var calculator = new VATCalculator();

            // Act
            var response = calculator.Calculate(request);

            // Assert
            Assert.Equal(100, response.Net);
            Assert.Equal(123, response.Gross);
            Assert.Equal(23, response.Vat);
        }

        [Fact]
        public void Calculate_WhenGrossIsProvided_ShouldReturnCorrectValues()
        {
            // Arrange
            var request = new VATRequest
            {
                Gross = 123,
                VatRate = 0.23
            };

            var calculator = new VATCalculator();

            // Act
            var response = calculator.Calculate(request);

            // Assert
            Assert.Equal(100, response.Net);
            Assert.Equal(123, response.Gross);
            Assert.Equal(23, response.Vat);
        }

        [Fact]
        public void Calculate_WhenVatIsProvided_ShouldReturnCorrectValues()
        {
            // Arrange
            var request = new VATRequest
            {
                Vat = 23,
                VatRate = 0.23
            };

            var calculator = new VATCalculator();

            // Act
            var response = calculator.Calculate(request);

            // Assert
            Assert.Equal(100, response.Net);
            Assert.Equal(123, response.Gross);
            Assert.Equal(23, response.Vat);
        }
    }
}
