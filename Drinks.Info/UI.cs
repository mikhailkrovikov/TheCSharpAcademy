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
            AnsiConsole.MarkupLine($"[green]Name:[/] {drink.StrDrink}");         
        }
    }
}
