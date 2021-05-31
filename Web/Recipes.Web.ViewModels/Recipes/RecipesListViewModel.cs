namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    public class RecipesListViewModel
    {
        public IEnumerable<RecipesViewModel> Recipes { get; set; }

        public int PageNumber { get; set; }

        public int RecipesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPage => this.PageNumber - 1;

        public int NextPage => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.RecipesCount / this.ItemsPerPage);
    }
}
