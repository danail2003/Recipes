namespace Recipes.Services.Data.SearchRecipes
{
    using System.Collections.Generic;

    public interface IIngredientsService
    {
        IEnumerable<T> GetAllPopular<T>();
    }
}
