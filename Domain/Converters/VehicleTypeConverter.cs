using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiccarCodeTest.Domain.Converters
{
    public class VehicleTypeConverter : JsonConverter<Vehicle>
    {
        /// <summary>
        /// The logic for determining which child type a vehicle should be deserialised into. 
        /// </summary>
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new KeyNotFoundException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "type")
            {
                throw new JsonException();
            }
            reader.Read();
            string typeDiscriminator = (string)reader.GetString();
            Vehicle vehicle = typeDiscriminator switch
            {
                "Car" => new Car(),
                "HGV" => new HGV(),
                _ => throw new NotSupportedException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return vehicle;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "numberOfSeats":
                            int NumberOfSeats = int.Parse(reader.GetString());
                            // Validation 
                            if (NumberOfSeats > 0)
                                ((Car)vehicle).NumberOfSeats = NumberOfSeats;
                            else
                                throw new JsonException("NumberOfSeats cannot be less than 1");
                            break;
                        case "maxTrailerLoad":
                            int MaxTrailerLoad = int.Parse(reader.GetString());
                                ((HGV)vehicle).MaxTrailerLoad = MaxTrailerLoad;
                            break;
                        case "registration":
                            string Registration = reader.GetString();
                            vehicle.Registration = Registration;
                            break;
                        case "totalTax":
                            int TotalTax = reader.GetInt32();
                            vehicle.SetTotalTax(TotalTax);
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { VehicleType.Car, typeof(Car) },
            { VehicleType.HGV, typeof(HGV) }
        };
    }
}
