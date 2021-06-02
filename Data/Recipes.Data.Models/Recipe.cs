namespace Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Recipes.Data.Common.Models;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.RecipeIngredient = new HashSet<RecipeIngredient>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public string OriginalUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
