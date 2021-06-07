namespace Recipes.Services.Data.SearchRecipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public IEnumerable<T> GetAllPopular<T>()
        {
            return this.ingredientsRepository.AllAsNoTracking().OrderBy(x => x.Name).Where(x => x.RecipeIngredients.Count() >= 15).To<T>().ToList();
        }
    }
}
