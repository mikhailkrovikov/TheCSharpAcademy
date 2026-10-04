namespace Drinks.Info;

internal class Program
{
    static async Task Main(string[] args)
    {
        var categories = await DrinkAPI.GetCategories();

        var category = UI.PintCategoryChoise(categories);

        List<Drink> drinks = await DrinkAPI.FilterByCategory(category);

        Drink drink = UI.PrintDrinkChoise(drinks);

        UI.PrintDrinkInfo(drink);

        await DrinkAPI.GetSomeInfo(drink);
    }

}
