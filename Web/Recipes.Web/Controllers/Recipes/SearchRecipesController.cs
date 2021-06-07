namespace Recipes.Web.Controllers.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Recipes.Services.Data.Recipes;
    using global::Recipes.Services.Data.SearchRecipes;
    using global::Recipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class SearchRecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IIngredientsService ingredientsService;

        public SearchRecipesController(
            IRecipesService recipesService,
            IIngredientsService ingredientsService)
        {
            this.recipesService = recipesService;
            this.ingredientsService = ingredientsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = this.ingredientsService.GetAllPopular<IngredientNameIdViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipesService.GetByIngredients<RecipesViewModel>(input.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
