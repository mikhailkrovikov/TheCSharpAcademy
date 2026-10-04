using Spectre.Console;
using System.Net.Http.Json;
using System.Text.Json;

namespace Drinks.Info
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var categories = await GetCategories();

            var category = UI.PintCategoryChoise(categories);

            List<Drink> drinks = await FilterByCategory(category);

            Drink drink = UI.PrintDrinkChoise(drinks);

            UI.PrintDrinkInfo(drink);

            await GetImage(drink);
        }

        static async Task GetImage(Drink drink)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drink.StrDrink}");
            foreach (var dr in drinks.Drinks)
            {
                if (drink == null) continue;
                using Stream imageSource = await client.GetStreamAsync(drink.StrDrinkThumb + "/small");
                var canvasImage = new CanvasImage(imageSource);
                //canvasImage.MaxWidth(80);
                AnsiConsole.Write(canvasImage);
            }
        }

        static async Task<List<Drink>> FilterByCategory(string category)
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            return drinks.Drinks?.ToList() ?? new List<Drink>();
        }

        static async Task<List<string>> GetCategories()
        {
            using var client = new HttpClient();
            var drinks = await client.GetFromJsonAsync<DrinksResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            return drinks.Drinks?.Select(d => d.StrCategory).ToList() ?? new List<string>();
        }
    }
}
