//using Newtonsoft.Json;

//using Powerplant.Models;

//namespace Powerplant.Services;

//public class FuelConverter : JsonConverter<Fuel>
//{
//    public override Fuel ReadJson(JsonReader reader, Type objectType, Fuel existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        var fuel = new Fuel();

//        while (reader.Read())
//        {
//            if (reader.TokenType == JsonToken.PropertyName)
//            {
//                var propertyName = reader.Value.ToString();
//                reader.Read(); // Move to the value
//                SetFuelProperty(fuel, propertyName, Convert.ToDouble(reader.Value));
//            }

//            if (reader.TokenType == JsonToken.EndObject)
//                break;
//        }

//        return fuel;
//    }

//    public override void WriteJson(JsonWriter writer, Fuel value, JsonSerializer serializer)
//    {
//        writer.WriteStartObject();
//        WriteFuelProperty(writer, "gas(euro/MWh)", value.Gas);
//        WriteFuelProperty(writer, "kerosine(euro/MWh)", value.Kerosine);
//        WriteFuelProperty(writer, "co2(euro/ton)", value.CO2);
//        WriteFuelProperty(writer, "wind(%)", value.Wind);
//        writer.WriteEndObject();
//    }

//    private void SetFuelProperty(Fuel fuel, string propertyName, double value)
//    {
//        switch (propertyName)
//        {
//            case "gas(euro/MWh)":
//                fuel.Gas = value;
//                break;
//            case "kerosine(euro/MWh)":
//                fuel.Kerosine = value;
//                break;
//            case "co2(euro/ton)":
//                fuel.CO2 = value;
//                break;
//            case "wind(%)":
//                fuel.Wind = value;
//                break;
//        }
//    }

//    private void WriteFuelProperty(JsonWriter writer, string propertyName, double value)
//    {
//        writer.WritePropertyName(propertyName);
//        writer.WriteValue(value);
//    }
//}
