using Spectre.Console;

namespace Drinks.Info
{
    public class UI
    {
        public const string BackToDrinks = "Back to drinks";
        public const string BackToCategories = "Back to categories";
        public const string AddToFavourites = "Add to favourites";
        public const string RemoveFromFavourites = "Remove from favourites";
        public const string FavouriteDrinks = "favour drinks";
        public const string ViewCategories = "view categories";
        public const string BackToMainMenu = "Back to main menu";
        public const string Exit = "exit";

        private const string Pink = "#ff8ad8";
        private const string Purple = "#bb9af7";

        private static void PrintScreen(string title)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[bold {Pink}]DRINKS.INFO[/]  [{Purple}]{Markup.Escape(title)}[/]")
                .RuleStyle(Purple)
                .LeftJustified());
            AnsiConsole.WriteLine();
        }

        private static SelectionPrompt<string> CreatePrompt(string title)
        {
            return new SelectionPrompt<string>()
                .Title($"[bold {Purple}]{title}[/]")
                .HighlightStyle(new Style(new Color(255, 138, 216), Color.Black, Decoration.Bold))
                .PageSize(10)
                .WrapAround()
                .MoreChoicesText($"[{Purple}]Use up/down to see more choices[/]");
        }

        private static Table CreateTable(string firstColumn, string secondColumn)
        {
            return new Table()
                .Border(TableBorder.Rounded)
                .BorderStyle(new Style(new Color(187, 154, 247)))
                .AddColumn($"[bold {Pink}]{firstColumn}[/]")
                .AddColumn($"[bold {Pink}]{secondColumn}[/]");
        }

        public static string PrintMainMenu()
        {
            PrintScreen("Main menu");
            return AnsiConsole.Prompt(
                CreatePrompt("Select an option:")
                    .AddChoices(FavouriteDrinks, ViewCategories, Exit));
        }

        public static string? PintCategoryChoise(List<string> categories)
        {
            PrintScreen("Categories");
            AnsiConsole.MarkupLine($"[{Purple}]Up/Down to navigate • Enter to select[/]");
            AnsiConsole.WriteLine();
            var choise = AnsiConsole.Prompt(
                CreatePrompt("Select a category:")
                    .AddChoices(categories)
                    .AddChoices(BackToMainMenu));
            return choise == BackToMainMenu ? null : choise;
        }

        public static Drink? PrintDrinkChoise(List<Drink> drinks, bool favourites = false)
        {
            PrintScreen(favourites ? FavouriteDrinks : "Drinks");
            if (favourites && drinks.Count == 0)
            {
                AnsiConsole.MarkupLine($"[{Purple}]No favourite drinks yet. Open a drink and select 'Add to favourites'.[/]");
                AnsiConsole.WriteLine();
            }
            var back = favourites ? BackToMainMenu : BackToCategories;
            var choise = AnsiConsole.Prompt(
                CreatePrompt("Select a drink:")
                    .AddChoices(drinks.Select(d => d.StrDrink).ToList())
                    .AddChoices(back));
            if (choise == back)
                return null;
            return drinks.First(d => d.StrDrink == choise);
        }

        public static string PrintNavigationChoise(bool favourites = false)
        {
            AnsiConsole.WriteLine();
            return AnsiConsole.Prompt(
                CreatePrompt("Where would you like to go next?")
                    .AddChoices(BackToDrinks, favourites ? BackToMainMenu : BackToCategories,
                        favourites ? RemoveFromFavourites : AddToFavourites, Exit));
        }

        public static void PrintDrinkInfo(Drink drink)
        {
            PrintScreen(drink.StrDrink ?? "Drink details");
            var table = CreateTable("Property", "Value")
                .AddRow("Name", Markup.Escape(drink.StrDrink ?? "N/A"))
                .AddRow("Category", Markup.Escape(drink.StrCategory ?? "N/A"))
                .AddRow("Alcoholic", Markup.Escape(drink.StrAlcoholic ?? "N/A"))
                .AddRow("Tags", Markup.Escape(drink.StrTags ?? "N/A"))
                .AddRow("Glass", Markup.Escape(drink.StrGlass ?? "N/A"));
            AnsiConsole.Write(table);
        }

        public static void PrintInstructions(string instructions)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold {Purple}]Instructions[/]");
            AnsiConsole.MarkupLine(Markup.Escape(instructions));
        }

        public static void PrintRecipe(List<string> ingredients, List<string> measures)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold {Purple}]Recipe[/]");
            var table = CreateTable("Ingredient", "Measure");
            for (int i = 0; i < ingredients.Count; i++)  
                if (!string.IsNullOrEmpty(ingredients[i]) || !string.IsNullOrEmpty(measures[i]))
                    table.AddRow(Markup.Escape(ingredients[i] ?? ""), Markup.Escape(measures[i] ?? ""));
            AnsiConsole.Write(table);
        }
        public static void PrintDrinkImage(string imageUrl)
        {
            using var client = new HttpClient();
            using var imageSource = client.GetStreamAsync(imageUrl).Result;
            var canvasImage = new CanvasImage(imageSource);
            canvasImage.MaxWidth(50);
            AnsiConsole.Write(canvasImage);
        }

        public static void SaveFavouriteDrink(Drink drink)
        {
            var storage = new FavouriteDrinkStorage();
            if (File.Exists("favourites.json"))
            {
                var json = File.ReadAllText("favourites.json");
                storage = System.Text.Json.JsonSerializer.Deserialize<FavouriteDrinkStorage>(json) ?? new FavouriteDrinkStorage();
            }
            if (storage.FavouriteDrinks == null)
                storage.FavouriteDrinks = new HashSet<Drink>();
            storage.FavouriteDrinks.Add(drink);
            var updatedJson = System.Text.Json.JsonSerializer.Serialize(storage, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("favourites.json", updatedJson);
        }
    }
}
