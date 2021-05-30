namespace Recipes.Web.Controllers.Recipes
{
    using System.Threading.Tasks;

    using global::Recipes.Data.Models;
    using global::Recipes.Services.Data.Categories;
    using global::Recipes.Services.Data.Recipes;
    using global::Recipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IGetCategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IGetCategoriesService categoriesService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipesInputModel
            {
                Categories = this.categoriesService.GetCategories(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRecipesInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = this.categoriesService.GetCategories();
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.recipesService.CreateAsync(inputModel, user.Id);

            return this.Redirect("/");
        }

        public IActionResult All(int id)
        {
            var viewModel = new RecipesListViewModel
            {
                PageNumber = id,
            };

            return this.View(viewModel);
        }
    }
}
