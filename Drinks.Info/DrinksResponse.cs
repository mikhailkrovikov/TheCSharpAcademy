using System.Text.Json.Serialization;

namespace Drinks.Info
{
    public record DrinksResponse
    {
        [JsonPropertyName("drinks")]
        public required List<Drink> Drinks { get; set; }
    }
}
