namespace Application.Services.UnitTests
{
    using Application.Services.Requests;
    using Xunit;

    [Trait("Category", "Application.Services.GrossCalculator.UnitTests")]
    public class GrossCalculatorTests
    {
        [Fact]
        public void Calculate_WhenGrossIsProvided_ShouldReturnCorrectValues()
        {
            // Arrange
            var request = new VATRequest
            {
                Gross = 123,
                VatRate = 0.23
            };

            var calculator = new GrossCalculator();

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

            var calculator = new GrossCalculator();

            // Act
            void action() => calculator.Calculate(request);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
