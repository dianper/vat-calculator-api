namespace Presentation.API.Controllers
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Responses;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v{version:apiVersion}/vat")]
    [ApiVersion("1.0")]
    public sealed class VATController(
        ICalculatorFactory factory,
        IValidator<VATRequest> validator) : ControllerBase
    {
        [HttpPost]
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

            var calculator = factory.CreateCalculator(request);
            var result = calculator.Calculate(request);
            return Ok(result);
        }
    }
}
