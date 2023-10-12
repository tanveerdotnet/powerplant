using Powerplant.Enums;
using Powerplant.Models;

namespace Powerplant.Services;

public class ProductionPlanCalculator : IProductionPlanCalculator
{
    public List<ProductionPlanOutput> CalculateProduction(Payload payload)
    {
        var productionPlan = new List<ProductionPlanOutput>();

        // Calculate wind power
        foreach (var windPlant in payload.Powerplants.Where(p => p.Type == PowerPlantType.windturbine))
        {
            var windPower = (windPlant.Pmax * payload.Fuels.Wind) / 100.0;
            productionPlan.Add(new ProductionPlanOutput { Name = windPlant.Name, Power = windPower });
        }

        // Calculate gas and turbojet power in order of decreasing efficiency.
        // This approach maximizes the usage of the most efficient power plants first. 
        var gasAndTurbojetPlants = payload.Powerplants
            .Where(p => p.Type == PowerPlantType.gasfired || p.Type == PowerPlantType.turbojet)
            .OrderByDescending(p => p.Efficiency);

        foreach (var plant in gasAndTurbojetPlants)
        {
            var power = CalculateGasOrTurbojetPower(plant, payload);
            productionPlan.Add(new ProductionPlanOutput { Name = plant.Name, Power = power });
        }

        // Order by power in descending order
        productionPlan = productionPlan.OrderByDescending(p => p.Power).ToList();

        return productionPlan;
    }

    private double CalculateGasOrTurbojetPower(PowerPlant plant, Payload payload)
    {
        var efficiency = plant.Efficiency;
        var fuelCost = plant.Type == PowerPlantType.gasfired ? payload.Fuels.Gas : payload.Fuels.Kerosine;

        var power = (efficiency * payload.Load) / fuelCost;

        // Ensure power is within Pmin and Pmax
        power = Math.Max(power, plant.Pmin);
        power = Math.Min(power, plant.Pmax);

        return Math.Round(power * 10) / 10;
    }
}