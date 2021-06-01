namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipesViewModel> Recipes { get; set; }
    }
}
