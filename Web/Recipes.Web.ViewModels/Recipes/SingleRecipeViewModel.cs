namespace Recipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public string ImageUrl { get; set; }

        public string OriginalUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CategoryRecipesCount { get; set; }

        public virtual string CategoryName { get; set; }

        public string AddedByUserUserName { get; set; }

        public IEnumerable<IngredientsViewModel> RecipeIngredient { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/recipes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
