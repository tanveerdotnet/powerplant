using Newtonsoft.Json;

using Powerplant.Services;

namespace Powerplant.Models;

public class Payload
{
    [JsonProperty("load")]
    public double Load { get; set; }

    [JsonProperty("fuels")]
    public Fuel Fuels { get; set; } = new();

    [JsonProperty("powerplants")]
    public List<PowerPlant> Powerplants { get; set; } = new();

    public string? Validate()
    {
        if (Load <= 0)
            return "Load should be a positive value.";

        if (Fuels == null)
            return "Invalid fuels data.";

        if (Fuels.Gas <= 0 || Fuels.Kerosine <= 0 || Fuels.CO2 <= 0)
            return "Fuel costs should be positive values.";

        if (Fuels.Wind < 0 || Fuels.Wind > 100)
            return "Wind percentage should be between 0 and 100.";

        if (Powerplants == null || Powerplants.Count == 0)
            return "Invalid power plant data.";

        return null; // Indicates successful validation
    }
}