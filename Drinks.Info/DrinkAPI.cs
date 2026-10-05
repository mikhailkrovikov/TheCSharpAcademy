using Spectre.Console;
using System.Net.Http.Json;

namespace Drinks.Info
{
    public static class DrinkAPI
    {
        public static async Task<List<string?>?> GetIngridients(Drink drink)
        {
            return GetDrinkInfo(drink)?.Result?.RefreshIngridients();
        }

        public static async Task<List<string?>?> GetMeasures(Drink drink)
        {
            return GetDrinkInfo(drink)?.Result?.RefreshMeasures();
        }

        public static async Task<List<Drink>?> FilterByCategory(string category)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            return drinks?.Drinks?.ToList();
        }

        public static async Task<List<string?>?> GetCategories()
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            return drinks?.Drinks?.Select(d => d.StrCategory).ToList();
        }

        public static async Task<Drink?> GetDrinkInfo(Drink drink)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drink.StrDrink}");
            if (drink == null)
                return null; var drinkInfo = drinks?.Drinks.FirstOrDefault();
            if (drinkInfo == null)
                return null;
            return drinkInfo;
        }
    }
}
