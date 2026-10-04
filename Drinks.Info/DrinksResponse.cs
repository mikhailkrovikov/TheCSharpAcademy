using System.Text.Json.Serialization;

namespace Drinks.Info
{
    public record DrinksResponse
    {
        [JsonPropertyName("drinks")]
        public List<Drink>? Drinks { get; set; }
    }
}
