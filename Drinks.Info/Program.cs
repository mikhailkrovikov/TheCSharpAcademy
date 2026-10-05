namespace Drinks.Info;

internal class Program
{
    static async Task Main(string[] args)
    {
        var categories = await DrinkAPI.GetCategories();

        var category = UI.PintCategoryChoise(categories);

        List<Drink> drinks = await DrinkAPI.FilterByCategory(category);

        Drink selectedDrink = UI.PrintDrinkChoise(drinks);



        //await DrinkAPI.GetSomeInfo(drink);
        var drink = await DrinkAPI.GetDrinkInfo(selectedDrink);
        UI.PrintDrinkInfo(drink);
        var ingridients = await DrinkAPI.GetIngridients(selectedDrink);
        var measures = await DrinkAPI.GetMeasures(selectedDrink);
        UI.PrintRecipe(ingridients, measures);
        UI.PrintDrinkImage(drink.StrDrinkThumb);
    }

}
