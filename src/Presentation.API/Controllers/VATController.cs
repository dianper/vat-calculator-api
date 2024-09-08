namespace Presentation.API.Controllers
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public sealed class VATController(
        IVATCalculator calculator,
        IValidator<VATRequest> validator,
        ILogger<VATController> logger) : ControllerBase
    {
        private const string UnexpectedErrorMessage = "An unexpected error occurred.";

        [HttpPost("calculate")]
        [ProducesResponseType<VATResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<VATResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<VATResponse>(StatusCodes.Status500InternalServerError)]
        public IActionResult CalculateVAT([FromBody] VATRequest request)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new VATResponse(errors));
            }

            try
            {
                var calculationResult = calculator.Calculate(request);
                return Ok(calculationResult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);
                return StatusCode(500, new VATResponse(UnexpectedErrorMessage));
            }
        }
    }
}
