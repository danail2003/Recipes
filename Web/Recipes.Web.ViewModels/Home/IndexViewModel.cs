namespace Recipes.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int CategoriesCount { get; set; }

        public int RecipesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int ImagesCount { get; set; }

        public IEnumerable<HomeRecipesViewModel> RandomRecipes { get; set; }
    }
}
