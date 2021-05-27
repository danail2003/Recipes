namespace Recipes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Recipes.Data;
    using Recipes.Web.ViewModels;
    using Recipes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                CategoriesCount = this.db.Categories.Count(),
                RecipesCount = this.db.Recipes.Count(),
                ImagesCount = this.db.Recipes.Count(),
                IngredientsCount = this.db.Ingredients.Count(),
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
