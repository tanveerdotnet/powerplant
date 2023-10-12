using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Powerplant.Models;
using Powerplant.Services;

namespace Powerplant.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductionPlanController : ControllerBase
{
    private readonly IProductionPlanCalculator _productionPlanCalculator;

    public ProductionPlanController(IProductionPlanCalculator productionPlanCalculator)
    {
        _productionPlanCalculator = productionPlanCalculator;
    }

    [HttpPost]
    [Route("calculateproduction")]
    public IActionResult CalculateProduction([FromBody] object jsonPayload)
    {
        if (jsonPayload is null || string.IsNullOrEmpty(Convert.ToString(jsonPayload)))
            return StatusCode(500, "Please provide valid data for calculating the production plan.");        
        else
        {
            // Deserialize the JSON string into a Payload object
            var payload = JsonConvert.DeserializeObject<Payload>(jsonPayload.ToString());

            var validationErrorMessage = payload?.Validate();
            if (!string.IsNullOrEmpty(validationErrorMessage))
                return BadRequest(validationErrorMessage);

            try
            {
                var productionPlan = _productionPlanCalculator.CalculateProduction(payload!);
                return Ok(productionPlan);
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return StatusCode(500, "An error occurred while calculating the production plan.");
            }
        }
    }
}