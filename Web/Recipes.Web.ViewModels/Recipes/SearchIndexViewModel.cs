namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class SearchIndexViewModel
    {
        public IEnumerable<IngredientNameIdViewModel> Ingredients { get; set; }
    }
}
