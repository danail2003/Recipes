namespace Recipes.Services.Data
{
    using System.Linq;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Data.Models;

    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public GetCountService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.imagesRepository = imagesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public CountsDto GetCounts()
        {
            CountsDto countsDto = new()
            {
                RecipesCount = this.recipesRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
            };

            return countsDto;
        }
    }
}
