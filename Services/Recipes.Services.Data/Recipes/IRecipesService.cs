namespace Recipes.Services.Data.Recipes
{
    using System.Threading.Tasks;

    using global::Recipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipesInputModel inputModel, string userId);
    }
}
