namespace Recipes.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;

    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public GetCategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetCategories()
        {
            return this.categoriesRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList()
            .OrderBy(x => x.Name)
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
