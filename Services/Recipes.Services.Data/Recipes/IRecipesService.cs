namespace Recipes.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::Recipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipesInputModel inputModel, string userId, string path);

        IEnumerable<T> GetAll<T>(int itemsPerPage, int page);

        int GetCount();

        IEnumerable<T> GetRandomRecipes<T>(int count);

        T GetRecipeById<T>(int id);

        Task UpdateAsync(int id, EditRecipeViewModel viewModel);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredients);
    }
}
