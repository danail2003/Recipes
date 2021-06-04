namespace Recipes.Services.Data.Votes
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVote(int recipeId, string userId, byte value);
    }
}
