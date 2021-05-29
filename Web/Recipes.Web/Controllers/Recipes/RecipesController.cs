namespace Recipes.Web.Controllers.Recipes
{
    using System.Threading.Tasks;

    using global::Recipes.Services.Data.Categories;
    using global::Recipes.Services.Data.Recipes;
    using global::Recipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IGetCategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public RecipesController(
            IGetCategoriesService categoriesService,
            IRecipesService recipesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipesInputModel();
            viewModel.Categories = this.categoriesService.GetCategories();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = this.categoriesService.GetCategories();
                return this.View(inputModel);
            }

            await this.recipesService.CreateAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
