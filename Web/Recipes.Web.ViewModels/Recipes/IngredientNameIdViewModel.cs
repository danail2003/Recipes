namespace Recipes.Web.ViewModels.Recipes
{
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
