using BL.Services.DTO;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Controllers
{
    [ApiController]
    [Route("api/decision")]
    public class DecisionController : ControllerBase
    {
        private readonly IDecisionService _decisionService;

        public DecisionController(IDecisionService decisionService)
        {
            _decisionService = decisionService;
        }

        // POST: api/decision/AddDecision/{caseId}
        [HttpPost("AddDecision/{caseId}")]
        public async Task<IActionResult> AddDecision(string caseId, [FromBody] DecisionDTO decision)
        {
            try
            {
                var decisionId = await _decisionService.AddDecision(caseId, decision);
                return CreatedAtAction(nameof(GetDecisions), new { caseId }, new { DecisionId = decisionId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add decision: {ex.Message}");
            }
        }

        // GET: api/decision/GetDecisions/{caseId}
        [HttpGet("GetDecisions/{caseId}")]
        public async Task<IActionResult> GetDecisions(string caseId)
        {
            try
            {
                var decisions = await _decisionService.GetDecisions(caseId);
                return Ok(decisions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve decisions: {ex.Message}");
            }
        }
    }
}
