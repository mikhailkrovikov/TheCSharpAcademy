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
                if (await .BrowseDrinks((await FavouriteDrinkStorage.LoadFavouriteDrinks()).ToList(), favourites: true))
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
                if (await FavouriteDrinkStorage.BrowseDrinks(drinks))
                    return;
            }
        }
    }
}
