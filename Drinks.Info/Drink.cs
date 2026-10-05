namespace Drinks.Info
{
    public class Drink
    {
        private List<string?>? Ingridients;
        private List<string?>? Measures;
        public int IdDrink { get; set; }
        public string? StrDrink { get; set; }
        public string? StrCategory { get; set; }
        public string? StrDrinkThumb { get; set; }
        public string? StrDrinkAlternate { get; set; }
        public string? StrTags { get; set; }
        public string? StrAlcoholic { get; set; }
        public string? StrGlass { get; set; }
        public string? StrInstructions { get; set; }


        public string? StrIngredient1 { get; set; }
        public string? StrIngredient2 { get; set; }
        public string? StrIngredient3 { get; set; }
        public string? StrIngredient4 { get; set; }
        public string? StrIngredient5 { get; set; }
        public string? StrIngredient6 { get; set; }
        public string? StrIngredient7 { get; set; }
        public string? StrIngredient8 { get; set; }
        public string? StrIngredient9 { get; set; }
        public string? StrIngredient10 { get; set; }
        public string? StrIngredient11 { get; set; }
        public string? StrIngredient12 { get; set; }
        public string? StrIngredient13 { get; set; }
        public string? StrIngredient14 { get; set; }
        public string? StrIngredient15 { get; set; }

        public string? StrMeasure1 { get; set; }
        public string? StrMeasure2 { get; set; }
        public string? StrMeasure3 { get; set; }
        public string? StrMeasure4 { get; set; }
        public string? StrMeasure5 { get; set; }
        public string? StrMeasure6 { get; set; }
        public string? StrMeasure7 { get; set; }
        public string? StrMeasure8 { get; set; }
        public string? StrMeasure9 { get; set; }
        public string? StrMeasure10 { get; set; }
        public string? StrMeasure11 { get; set; }
        public string? StrMeasure12 { get; set; }
        public string? StrMeasure13 { get; set; }
        public string? StrMeasure14 { get; set; }
        public string? StrMeasure15 { get; set; }

        public List<string?> RefreshIngridients()
        {
            Ingridients = new List<string?>
            {
                StrIngredient1,
                StrIngredient2,
                StrIngredient3,
                StrIngredient4,
                StrIngredient5,
                StrIngredient6,
                StrIngredient7,
                StrIngredient8,
                StrIngredient9,
                StrIngredient10,
                StrIngredient11,
                StrIngredient12,
                StrIngredient13,
                StrIngredient14,
                StrIngredient15
            };
            return Ingridients;
        }

        public List<string?> RefreshMeasures()
        {
            Measures = new List<string?>
            {
                StrMeasure1,
                StrMeasure2,
                StrMeasure3,
                StrMeasure4,
                StrMeasure5,
                StrMeasure6,
                StrMeasure7,
                StrMeasure8,
                StrMeasure9,
                StrMeasure10,
                StrMeasure11,
                StrMeasure12,
                StrMeasure13,
                StrMeasure14,
                StrMeasure15
            };
            return Measures;
        }
    }
}
