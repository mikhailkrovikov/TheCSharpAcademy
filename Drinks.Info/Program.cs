namespace Drinks.Info;

internal class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            var option = UI.PrintMainMenu();
            if (option == UI.Exit)
                return;

            if (option == UI.FavouriteDrinks)
            {
                if (await BrowseDrinks((await UI.LoadFavouriteDrinks()).ToList(), favourites: true))
                    return;
                continue;
            }

            var categories = await DrinkAPI.GetCategories();
            while (true)
            {
                var category = UI.PintCategoryChoise(categories);
                if (category == null)
                    break;

                var drinks = await DrinkAPI.FilterByCategory(category);
                if (await BrowseDrinks(drinks))
                    return;
            }
        }
    }

    private static async Task<bool> BrowseDrinks(List<Drink> drinks, bool favourites = false)
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
                    await UI.RemoveFavouriteDrink(selectedDrink);
                    drinks = (await UI.LoadFavouriteDrinks()).ToList();
                    break;
                }
                if (navigation == UI.AddToFavourites)
                    UI.SaveFavouriteDrink(drink);
            }
        }
    }

}
