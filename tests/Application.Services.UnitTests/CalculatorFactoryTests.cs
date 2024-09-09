namespace Application.Services.UnitTests
{
    using Application.Services.Exceptions;
    using Application.Services.Requests;
    using Xunit;

    [Trait("Category", "Application.Services.CalculatorFactory.UnitTests")]
    public sealed class CalculatorFactoryTests
    {
        [Fact]
        public void CreateCalculator_WhenNetIsProvided_ShouldReturnNetCalculator()
        {
            // Arrange
            var request = new VATRequest
            {
                Net = 100,
                VatRate = 0.23
            };

            var factory = new CalculatorFactory();

            // Act
            var calculator = factory.CreateCalculator(request);

            // Assert
            Assert.IsType<NetCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_WhenGrossIsProvided_ShouldReturnGrossCalculator()
        {
            // Arrange
            var request = new VATRequest
            {
                Gross = 123,
                VatRate = 0.23
            };

            var factory = new CalculatorFactory();

            // Act
            var calculator = factory.CreateCalculator(request);

            // Assert
            Assert.IsType<GrossCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_WhenVatIsProvided_ShouldReturnVATCalculator()
        {
            // Arrange
            var request = new VATRequest
            {
                Vat = 23,
                VatRate = 0.23
            };

            var factory = new CalculatorFactory();

            // Act
            var calculator = factory.CreateCalculator(request);

            // Assert
            Assert.IsType<VATCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_WhenNoValueIsProvided_ShouldThrowInvalidCalculatorException()
        {
            // Arrange
            var request = new VATRequest();

            var factory = new CalculatorFactory();

            // Act
            void Act() => factory.CreateCalculator(request);

            // Assert
            Assert.Throws<InvalidCalculatorException>(Act);
        }
    }
}
