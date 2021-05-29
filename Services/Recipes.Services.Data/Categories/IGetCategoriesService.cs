namespace Recipes.Services.Data.Categories
{
    using System.Collections.Generic;

    public interface IGetCategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetCategories();
    }
}
