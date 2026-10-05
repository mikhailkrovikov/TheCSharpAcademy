using Spectre.Console;

namespace Drinks.Info
{
    public class UI
    {
        public static string PintCategoryChoise(List<string> categories)
        {
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a [green]category[/]:")
                    .AddChoices(categories));
            return choise;
        }

        public static Drink PrintDrinkChoise(List<Drink> drinks)
        {
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a [purple]drink[/]:")
                    .AddChoices(drinks.Select(d => d.StrDrink).ToList()));
            return drinks.First(d => d.StrDrink == choise);
        }

        public static void PrintDrinkInfo(Drink drink)
        {
            var table = new Table()
                .AddColumn("Property")
                .AddColumn("Value")
                .AddRow("Name", drink.StrDrink ?? "N/A")
                .AddRow("Category", drink.StrCategory ?? "N/A")
                .AddRow("Alcoholic", drink.StrAlcoholic ?? "N/A")
                .AddRow("Tags", drink.StrTags ?? "N/A")
                .AddRow("Instructions", drink.StrInstructions ?? "N/A")
                .AddRow("Glass", drink.StrGlass ?? "N/A");
            AnsiConsole.Write(table);
        }

        public static void PrintRecipe(List<string> ingredients, List<string> measures)
        {
            var table = new Table().AddColumn("Ingredient").AddColumn("Measure");
            for (int i = 0; i < ingredients.Count; i++)  
                if (!string.IsNullOrEmpty(ingredients[i]) || !string.IsNullOrEmpty(measures[i]))
                    table.AddRow(ingredients[i] ?? "", measures[i] ?? ""); 
            AnsiConsole.Write(table);
        }
        public static void PrintDrinkImage(string imageUrl)
        {
            using var client = new HttpClient();
            using var imageSource = client.GetStreamAsync(imageUrl).Result;
            var canvasImage = new CanvasImage(imageSource);
            AnsiConsole.Write(canvasImage);
        }
    }
}
