namespace Recipes.Data.Models
{
    using System.Collections.Generic;

    using Recipes.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
