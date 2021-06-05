namespace Recipes.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using global::Recipes.Data.Common.Repositories;
    using global::Recipes.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int recipeId)
        {
            return this.votesRepository.All().Where(x => x.RecipeId == recipeId).Average(x => x.Value);
        }

        public async Task SetVote(int recipeId, string userId, byte value)
        {
            var vote = this.votesRepository.All().FirstOrDefault(x => x.RecipeId == recipeId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
