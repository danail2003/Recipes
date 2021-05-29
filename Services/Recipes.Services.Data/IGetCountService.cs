namespace Recipes.Services.Data
{
    using global::Recipes.Services.Data.Models;

    public interface IGetCountService
    {
        CountsDto GetCounts();
    }
}
