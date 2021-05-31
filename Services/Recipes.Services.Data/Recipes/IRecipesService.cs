namespace Recipes.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::Recipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipesInputModel inputModel, string userId);

        IEnumerable<T> GetAll<T>(int itemsPerPage, int page);
    }
}
