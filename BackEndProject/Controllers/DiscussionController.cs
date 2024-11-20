using BL.Services.DTO;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Controllers
{
    [ApiController]
    [Route("api/discussion")]
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionService _discussionService;

        public DiscussionController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        // POST: api/discussion/AddDiscussion
        [HttpPost("AddDiscussion/{caseId}")]
        public async Task<IActionResult> AddDiscussion(string caseId, [FromBody] DiscussionDTO discussion)
        {
            try
            {
                var discussionId = await _discussionService.AddDiscussion(caseId, discussion);
                return CreatedAtAction(nameof(GetDiscussions), new { caseId }, new { DiscussionId = discussionId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add discussion: {ex.Message}");
            }
        }

        // GET: api/discussion/GetDiscussions/{caseId}
        [HttpGet("GetDiscussions/{caseId}")]
        public async Task<IActionResult> GetDiscussions(string caseId)
        {
            try
            {
                var discussions = await _discussionService.GetDiscussions(caseId);
                return Ok(discussions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve discussions: {ex.Message}");
            }
        }
    }
}
