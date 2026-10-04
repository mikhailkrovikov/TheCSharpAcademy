using Spectre.Console;
using System.Net.Http.Json;

namespace Drinks.Info
{
    public static class DrinkAPI
    {
        public static async Task GetSomeInfo(Drink drink)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drink.StrDrink}");
            foreach (var dr in drinks.Drinks)
            {
                if (dr == null)
                    continue; 
                //using var imageSource = await client.GetStreamAsync(dr.StrDrinkThumb + "/small");
                //var canvasImage = new CanvasImage(imageSource);
                //AnsiConsole.Write(canvasImage);
                AnsiConsole.MarkupLine(dr.StrInstructions);
            }
        }

        public static async Task<List<Drink>> FilterByCategory(string category)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            return drinks.Drinks?.ToList() ?? new List<Drink>();
        }

        public static async Task<List<string>> GetCategories()
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            return drinks.Drinks?.Select(d => d.StrCategory).ToList() ?? new List<string>();
        }
    }
}
