namespace Recipes.Services.Data.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class VotesService : IVotesService
    {
        public Task SetVote(int recipeId, string userId, byte value)
        {
            throw new NotImplementedException();
        }
    }
}
