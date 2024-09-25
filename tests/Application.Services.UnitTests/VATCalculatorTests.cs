namespace Application.Services.UnitTests
{
    using Application.Services.Requests;
    using Xunit;

    [Trait("Category", "Application.Services.VATCalculator.UnitTests")]
    public sealed class VATCalculatorTests
    {
        [Fact]
        public void Calculate_WhenVatIsProvided_ShouldReturnCorrectValues()
        {
            // Arrange
            var request = new VATRequest
            {
                Vat = 23,
                VatRate = 0.23m
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
        public void Calculate_WhenRequestIsInvalid_ShouldThrowArgumentNullException()
        {
            // Arrange
            var request = new VATRequest();

            var calculator = new VATCalculator();

            // Act
            void action() => calculator.Calculate(request);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
