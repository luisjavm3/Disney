using System.Text.Json.Serialization;

namespace Disney.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Order
    {
        ASC,
        DESC,
        None
    }
}