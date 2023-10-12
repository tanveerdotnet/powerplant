using Powerplant.Enums;

namespace Powerplant.Models;

public class PowerPlant
{
    public string Name { get; set; } = string.Empty;
    public PowerPlantType Type { get; set; }
    public double Efficiency { get; set; }
    public double Pmax { get; set; }
    public double Pmin { get; set; }
}
