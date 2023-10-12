using Powerplant.Models;

namespace Powerplant.Services;

public interface IProductionPlanCalculator
{
    List<ProductionPlanOutput> CalculateProduction(Payload payload);
}