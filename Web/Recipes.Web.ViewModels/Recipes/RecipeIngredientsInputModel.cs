﻿namespace Recipes.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeIngredientsInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Quantity { get; set; }
    }
}
