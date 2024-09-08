namespace Presentation.API.UnitTests.Controllers
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Presentation.API.Controllers;
    using Xunit;

    [Trait("Category", "Presentation.API.Controllers.VATController.UnitTests")]
    public sealed class VATControllerTests
    {
        private readonly Mock<IVATCalculator> calculator;
        private readonly Mock<IValidator<VATRequest>> validator;
        private readonly Mock<ILogger<VATController>> logger;
        private readonly VATController controller;

        public VATControllerTests()
        {
            this.calculator = new Mock<IVATCalculator>();
            this.validator = new Mock<IValidator<VATRequest>>();
            this.logger = new Mock<ILogger<VATController>>();
            this.controller = new VATController(
                this.calculator.Object,
                this.validator.Object,
                this.logger.Object);
        }

        [Fact]
        public void CalculateVAT_WhenRequestIsValid_ShouldReturnOk()
        {
            // Arrange
            var request = new VATRequest
            {
                Net = 100,
                VatRate = 0.23
            };

            this.calculator
                .Setup(c => c.Calculate(It.IsAny<VATRequest>()))
                .Returns(new VATResponse(100, 123, 23));

            this.validator
                .Setup(v => v.Validate(It.IsAny<VATRequest>()))
                .Returns(new ValidationResult());

            // Act
            var result = this.controller.CalculateVAT(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<VATResponse>(okResult.Value);

            Assert.Equal(100, response.Net);
            Assert.Equal(123, response.Gross);
            Assert.Equal(23, response.Vat);
        }

        [Fact]
        public void CalculateVAT_WhenRequestIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new VATRequest
            {
                VatRate = 0.23
            };

            this.calculator
                .Setup(c => c.Calculate(It.IsAny<VATRequest>()))
                .Returns(new VATResponse("Net is required"));

            this.validator
                .Setup(v => v.Validate(It.IsAny<VATRequest>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Net", "Net is required.")
                }));

            // Act
            var result = this.controller.CalculateVAT(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<VATResponse>(badRequestResult.Value);

            Assert.Equal("Net is required.", response.Message);
        }

        [Fact]
        public void CalculateVAT_WhenCalculatorThrowsException_ShouldReturnInternalServerError()
        {
            // Arrange
            var request = new VATRequest
            {
                Net = 100,
                VatRate = 0.23
            };

            this.calculator
                .Setup(c => c.Calculate(It.IsAny<VATRequest>()))
                .Throws(new Exception("An error occurred."));

            this.validator
                .Setup(v => v.Validate(It.IsAny<VATRequest>()))
                .Returns(new ValidationResult());

            // Act
            var result = this.controller.CalculateVAT(request);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsType<VATResponse>(internalServerErrorResult.Value);

            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("An unexpected error occurred.", response.Message);

            logger.VerifyAll();
        }
    }
}
