using Spectre.Console;
using System.Net.Http.Json;

namespace Drinks.Info
{
    public static class DrinkAPI
    {
        private static HttpClient _httpClient = new();
        private static Drink _drinkInfo;

        public static async Task<List<string?>?> GetIngridients(Drink drink)
        {
            return _drinkInfo?.RefreshIngridients();
        }

        public static async Task<List<string?>?> GetMeasures(Drink drink)
        {
            return _drinkInfo?.RefreshMeasures();
        }

        public static async Task<List<Drink>?> FilterByCategory(string category)
        {     
            var drinks = await _httpClient.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            return drinks?.Drinks?.ToList();
        }

        public static async Task<List<string?>?> GetCategories()
        {
            var drinks = await _httpClient.GetFromJsonAsync<DrinksResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            return drinks?.Drinks?.Select(d => d.StrCategory).ToList();
        }

        public static async Task<Drink?> GetDrinkInfo(Drink drink)
        {
            var drinks = await _httpClient.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drink.StrDrink}");
            if (drink == null) return null; 

            var drinkInfo = drinks?.Drinks.FirstOrDefault();
            if (drinkInfo == null) return null;

            _drinkInfo = drinkInfo;
            return drinkInfo;
        }
    }
}
