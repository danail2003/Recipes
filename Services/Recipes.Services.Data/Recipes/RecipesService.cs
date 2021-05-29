﻿namespace Recipes.Services.Data.Recipes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;
    using global::Recipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipesInputModel inputModel)
        {
            var recipe = new Recipe
            {
                Name = inputModel.Name,
                PortionsCount = inputModel.PortionsCount,
                PreparationTime = TimeSpan.FromMinutes(inputModel.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(inputModel.CookingTime),
                CategoryId = inputModel.CategoryId,
                Instructions = inputModel.Instructions,
            };

            foreach (var ingredient in inputModel.Ingredients)
            {
                var newIngredient = this.ingredientsRepository.All().FirstOrDefault(x => x.Name == ingredient.Name);

                if (newIngredient == null)
                {
                    newIngredient = new Ingredient
                    {
                        Name = ingredient.Name,
                    };
                }

                recipe.RecipeIngredient.Add(new RecipeIngredient
                {
                    Ingredient = newIngredient,
                    Quantity = ingredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}