namespace Recipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateRecipesInputModel : BaseRecipeInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<RecipeIngredientsInputModel> Ingredients { get; set; }
    }
}
