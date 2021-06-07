namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class ListViewModel
    {
        public IEnumerable<RecipesViewModel> Recipes { get; set; }
    }
}
