namespace Recipes.Services.Data.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;
    using global::Recipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jpeg" };
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipesInputModel inputModel, string userId, string path)
        {
            var recipe = new Recipe
            {
                Name = inputModel.Name,
                PortionsCount = inputModel.PortionsCount,
                PreparationTime = TimeSpan.FromMinutes(inputModel.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(inputModel.CookingTime),
                CategoryId = inputModel.CategoryId,
                Instructions = inputModel.Instructions,
                AddedByUserId = userId,
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

            Directory.CreateDirectory($"{path}/recipes/");

            foreach (var image in inputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new InvalidOperationException($"This extension '{extension}' is not valid!");
                }

                Image dbImage = new()
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                recipe.Images.Add(dbImage);

                var physicalPath = $"{path}/recipes/{dbImage.Id}.{extension}";
                using Stream stream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(stream);
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int itemsPerPage, int page)
        {
            return this.recipesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredients)
        {
            var query = this.recipesRepository.All().AsQueryable();

            foreach (var ingredientId in ingredients)
            {
                query = query.Where(x => x.RecipeIngredient.Any(x => x.IngredientId == ingredientId));
            }

            return query.To<T>().ToList();
        }

        public int GetCount()
        {
            return this.recipesRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetRandomRecipes<T>(int count)
        {
            return this.recipesRepository.All().OrderBy(x => Guid.NewGuid()).Take(count).To<T>().ToList();
        }

        public T GetRecipeById<T>(int id)
        {
            return this.recipesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public async Task UpdateAsync(int id, EditRecipeViewModel viewModel)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);

            recipe.Name = viewModel.Name;
            recipe.Instructions = viewModel.Instructions;
            recipe.PortionsCount = viewModel.PortionsCount;
            recipe.CookingTime = TimeSpan.FromMinutes(viewModel.CookingTime);
            recipe.PreparationTime = TimeSpan.FromMinutes(viewModel.PreparationTime);
            recipe.CategoryId = viewModel.CategoryId;

            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
