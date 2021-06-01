namespace Recipes.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
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
