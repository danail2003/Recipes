namespace Recipes.Web.Controllers
{
    using System.Diagnostics;

    using global::Recipes.Services.Data;
    using global::Recipes.Services.Data.Models;
    using global::Recipes.Services.Data.Recipes;
    using global::Recipes.Web.ViewModels;
    using global::Recipes.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountService countService;
        private readonly IRecipesService recipesService;

        public HomeController(
            IGetCountService countService,
            IRecipesService recipesService)
        {
            this.countService = countService;
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {
            CountsDto countsDto = this.countService.GetCounts();

            IndexViewModel viewModel = new()
            {
                RecipesCount = countsDto.RecipesCount,
                ImagesCount = countsDto.ImagesCount,
                IngredientsCount = countsDto.IngredientsCount,
                CategoriesCount = countsDto.CategoriesCount,
                RandomRecipes = this.recipesService.GetRandomRecipes<HomeRecipesViewModel>(10),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
