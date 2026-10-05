namespace Drinks.Info;

internal class Program
{
    static async Task Main(string[] args)
    {
        var categories = await DrinkAPI.GetCategories();

        while (true)
        {
            var category = UI.PintCategoryChoise(categories);
            if (category == null)
                return;

            List<Drink> drinks = await DrinkAPI.FilterByCategory(category);

            while (true)
            {
                var selectedDrink = UI.PrintDrinkChoise(drinks);
                if (selectedDrink == null)
                    break;

                var drink = await DrinkAPI.GetDrinkInfo(selectedDrink);
                UI.PrintDrinkInfo(drink);
                var ingridients = await DrinkAPI.GetIngridients(selectedDrink);
                var measures = await DrinkAPI.GetMeasures(selectedDrink);
                UI.PrintRecipe(ingridients, measures);
                UI.PrintDrinkImage(drink.StrDrinkThumb + "/small");

                var navigation = UI.PrintNavigationChoise();
                if (navigation == UI.Exit)
                    return;
                if (navigation == UI.BackToCategories)
                    break;
            }
        }
    }

}
