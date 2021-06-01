namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateRecipesInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Instructions { get; set; }

        [Range(1, 3 * 60 * 60)]
        public int PreparationTime { get; set; }

        [Range(1, 5 * 60 * 60)]
        public int CookingTime { get; set; }

        [Range(1, 50)]
        public int PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<RecipeIngredientsInputModel> Ingredients { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
