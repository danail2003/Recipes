namespace Recipes.Data.Models
{
    using System.Collections.Generic;

    using Recipes.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
