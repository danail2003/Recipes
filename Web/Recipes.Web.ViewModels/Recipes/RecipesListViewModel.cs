namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipesListViewModel
    {
        public IEnumerable<RecipesViewModel> Recipes { get; set; }

        public int PageNumber { get; set; }
    }
}
