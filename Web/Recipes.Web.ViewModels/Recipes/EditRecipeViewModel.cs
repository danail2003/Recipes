namespace Recipes.Web.ViewModels.Recipes
{
    using AutoMapper;
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class EditRecipeViewModel : BaseRecipeInputModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, EditRecipeViewModel>()
                .ForMember(x => x.CookingTime, opt =>
                opt.MapFrom(x => (int)x.CookingTime.TotalMinutes))
                .ForMember(x => x.PreparationTime, opt =>
                opt.MapFrom(x => (int)x.PreparationTime.TotalMinutes));
        }
    }
}
