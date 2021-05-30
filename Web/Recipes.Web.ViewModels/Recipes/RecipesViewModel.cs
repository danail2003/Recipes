namespace Recipes.Web.ViewModels.Recipes
{
    public class RecipesViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
