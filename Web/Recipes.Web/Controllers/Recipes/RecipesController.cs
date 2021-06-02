namespace Recipes.Web.Controllers.Recipes
{
    using System;
    using System.Threading.Tasks;

    using global::Recipes.Data.Models;
    using global::Recipes.Services.Data.Categories;
    using global::Recipes.Services.Data.Recipes;
    using global::Recipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IGetCategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public RecipesController(
            IGetCategoriesService categoriesService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.environment = environment;
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

                await this.recipesService.CreateAsync(inputModel, user.Id, $"{this.environment.WebRootPath}/images");
           /* try
            {
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                inputModel.Categories = this.categoriesService.GetCategories();
                return this.View(inputModel);
            }*/

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id < 1)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                RecipesCount = this.recipesService.GetCount(),
                PageNumber = id,
                Recipes = this.recipesService.GetAll<RecipesViewModel>(ItemsPerPage, id),
            };

            return this.View(viewModel);
        }
    }
}
