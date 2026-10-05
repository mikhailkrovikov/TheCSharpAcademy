namespace Drinks.Info;


public class FavouriteDrinkStorage
{
    public HashSet<Drink> FavouriteDrinks { get; set; }

    public static async Task RemoveFavouriteDrink(Drink drink)
    {
        var favourites = await LoadFavouriteDrinks();
        favourites.RemoveWhere(saved => drink.IdDrink != 0
            ? saved.IdDrink == drink.IdDrink
            : saved.StrDrink == drink.StrDrink);
        var storage = new FavouriteDrinkStorage { FavouriteDrinks = favourites };
        var json = System.Text.Json.JsonSerializer.Serialize(storage,
            new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("favourites.json", json);
    }

    public static async Task<HashSet<Drink>> LoadFavouriteDrinks()
    {
        if (!File.Exists("favourites.json"))
            return new HashSet<Drink>();
        var json = await File.ReadAllTextAsync("favourites.json");
        var storage = System.Text.Json.JsonSerializer.Deserialize<FavouriteDrinkStorage>(json);
        return storage?.FavouriteDrinks ?? new HashSet<Drink>();
    }
    public static async Task<bool> BrowseDrinks(List<Drink> drinks, bool favourites = false)
    {
        while (true)
        {
            var selectedDrink = UI.PrintDrinkChoise(drinks, favourites);
            if (selectedDrink == null)
                return false;

            var drink = await DrinkAPI.GetDrinkInfo(selectedDrink);
            UI.PrintDrinkInfo(drink);
            var ingridients = await DrinkAPI.GetIngridients(selectedDrink);
            var measures = await DrinkAPI.GetMeasures(selectedDrink);
            UI.PrintRecipe(ingridients, measures);
            UI.PrintInstructions(drink.StrInstructions ?? "N/A");
            UI.PrintDrinkImage(drink.StrDrinkThumb + "/small");

            while (true)
            {
                var navigation = UI.PrintNavigationChoise(favourites);
                if (navigation == UI.Exit)
                    return true;
                if (navigation == UI.BackToCategories || navigation == UI.BackToMainMenu)
                    return false;
                if (navigation == UI.BackToDrinks)
                    break;
                if (navigation == UI.RemoveFromFavourites && favourites)
                {
                    await FavouriteDrinkStorage.RemoveFavouriteDrink(selectedDrink);
                    drinks = (await FavouriteDrinkStorage.LoadFavouriteDrinks()).ToList();
                    break;
                }
                if (navigation == UI.AddToFavourites)
                    UI.SaveFavouriteDrink(drink);
            }
        }
    }
}
