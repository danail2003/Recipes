namespace Recipes.Web.Controllers
{
    using System.Threading.Tasks;

    using global::Recipes.Services;
    using Microsoft.AspNetCore.Mvc;

    public class GatherRecipesController : BaseController
    {
        private readonly IGotvachBgScraperService gotvachBgScraperService;

        public GatherRecipesController(IGotvachBgScraperService gotvachBgScraperService)
        {
            this.gotvachBgScraperService = gotvachBgScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.gotvachBgScraperService.PopulateDbWithRecipesAsync(300);

            return this.RedirectToAction("Index");
        }
    }
}
