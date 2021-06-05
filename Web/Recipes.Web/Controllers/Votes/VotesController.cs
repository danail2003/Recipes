namespace Recipes.Web.Controllers.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using global::Recipes.Services.Data.Votes;
    using global::Recipes.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel inputModel)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetVote(inputModel.RecipeId, userId, inputModel.Value);

            double averageVote = this.votesService.GetAverageVotes(inputModel.RecipeId);

            return new PostVoteResponseModel { AverageVote = averageVote };
        }
    }
}
