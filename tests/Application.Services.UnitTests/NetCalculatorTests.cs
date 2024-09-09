namespace Application.Services.UnitTests
{
    using Application.Services.Requests;
    using System;
    using Xunit;

    [Trait("Category", "Application.Services.NetCalculator.UnitTests")]
    public sealed class NetCalculatorTests
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

            var calculator = new NetCalculator();

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

            var calculator = new NetCalculator();

            // Act
            void action() => calculator.Calculate(request);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
