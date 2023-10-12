using Powerplant.Enums;
using Powerplant.Models;

namespace Powerplant.Services;

public class ProductionPlanCalculator : IProductionPlanCalculator
{
    public List<ProductionPlanOutput> CalculateProduction(Payload payload)
    {
        List<ProductionPlanOutput> productionPlan = new List<ProductionPlanOutput>();

        // Calculate wind power
        double windPower = (payload.Powerplants
            .Where(pp => pp.Type == PowerPlantType.windturbine)
            .Sum(pp => (pp.Pmax * payload.Fuels.Wind) / 100.0)) * payload.Fuels.Wind / 100.0;

        // Allocate wind power to wind turbines
        foreach (var windPlant in payload.Powerplants.Where(pp => pp.Type == PowerPlantType.windturbine))
        {
            double windAllocation = (windPlant.Pmax * payload.Fuels.Wind) / 100.0;
            productionPlan.Add(new ProductionPlanOutput
            {
                Name = windPlant.Name,
                Power = Math.Round(windAllocation, 2)
            });
        }

        // Calculate gas-fired and turbojet power
        double remainingLoad = payload.Load - windPower;
        var gasAndTurbojetPlants = payload.Powerplants
            .Where(pp => pp.Type == PowerPlantType.gasfired || pp.Type == PowerPlantType.turbojet)
            .OrderByDescending(pp => pp.Efficiency);

        foreach (var plant in gasAndTurbojetPlants)
        {
            double efficiency = plant.Efficiency;
            double costPerMWh = 0;

            if (plant.Type == PowerPlantType.gasfired)
            {
                costPerMWh = payload.Fuels.Gas / efficiency;
            }
            else if (plant.Type == PowerPlantType.turbojet)
            {
                costPerMWh = payload.Fuels.Kerosine / efficiency;
            }

            double power = Math.Max(plant.Pmin, Math.Min(remainingLoad, plant.Pmax));

            productionPlan.Add(new ProductionPlanOutput
            {
                Name = plant.Name,
                Power = Math.Round(power, 2),
            });

            remainingLoad -= power;

            if (remainingLoad <= 0)
                break;
        }

        // Set remaining gas and turbojet plants to 0 power
        foreach (var plant in gasAndTurbojetPlants)
        {
            if (productionPlan.All(pp => pp.Name != plant.Name))
            {
                productionPlan.Add(new ProductionPlanOutput
                {
                    Name = plant.Name,
                    Power = 0.0,
                });
            }
        }

        return productionPlan;
    }
}