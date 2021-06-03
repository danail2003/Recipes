namespace Recipes.Web.ViewModels.Recipes
{
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
