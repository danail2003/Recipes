namespace Recipes.Web.Controllers.Recipes
{
    using System;
    using System.Threading.Tasks;

    using global::Recipes.Common;
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

            try
            {
                await this.recipesService.CreateAsync(inputModel, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                inputModel.Categories = this.categoriesService.GetCategories();
                return this.View(inputModel);
            }

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var viewModel = this.recipesService.GetRecipeById<EditRecipeViewModel>(id);
            viewModel.Categories = this.categoriesService.GetCategories();

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecipeViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.recipesService.GetRecipeById<EditRecipeViewModel>(id);
                model.Categories = this.categoriesService.GetCategories();

                return this.View(model);
            }

            await this.recipesService.UpdateAsync(id, viewModel);

            return this.RedirectToAction(nameof(this.ById), new { id });
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

        public IActionResult ById(int id)
        {
            var recipe = this.recipesService.GetRecipeById<SingleRecipeViewModel>(id);

            return this.View(recipe);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
