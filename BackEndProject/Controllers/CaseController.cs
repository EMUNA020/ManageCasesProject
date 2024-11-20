using BL.Services;
using BL.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Controllers
{
    [ApiController]
    [Route("api/Case")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CaseController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        // GET: api/case
        [HttpGet("GetAllCases")]
        public async Task<IActionResult> GetAllCases()
        {
            var cases = await _caseService.GetAllCases();
            return Ok(cases);
        }
        // GET: api/case/GetCaseById/{id}
        [HttpGet("GetCaseById/{id}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            try
            {
                var caseDetails = await _caseService.GetCaseById(id);
                return Ok(caseDetails);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Case not found");
            }
        }

        // POST: api/case/CreateCase
        [HttpPost("CreateCase")]
        public async Task<IActionResult> CreateCase([FromBody] CaseDTO caseDTO)
        {
            try
            {
                var createdCase = await _caseService.CreateCase(caseDTO);
                return CreatedAtAction(nameof(GetCaseById), new { id = createdCase.Id }, createdCase);
            }
            catch
            {
                return BadRequest("Failed to create the case");
            }
        }

        // PUT: api/case/UpdateCase
        [HttpPut("UpdateCase")]
        public async Task<IActionResult> UpdateCase([FromBody] CaseDTO caseDTO)
        {
            try
            {
                var updatedCase = await _caseService.UpdateCase(caseDTO);
                return Ok(updatedCase);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Case not found");
            }
            catch
            {
                return BadRequest("Failed to update the case");
            }
        }

        // DELETE: api/case/DeleteCase/{id}/{userId}
        [HttpDelete("DeleteCase/{id}/{userId}")]
        public async Task<IActionResult> DeleteCase(int id, int userId)
        {
            var result = await _caseService.DeleteCase(id, userId);

            if (!result.success ?? false)
                return NotFound(result.data);

            return Ok(result.data);
        }

        // PATCH: api/case/UpdateCaseStatus/{caseId}
        [HttpPatch("UpdateCaseStatus/{caseId}")]
        public async Task<IActionResult> UpdateCaseStatus(int caseId, [FromBody] string newStatus)
        {
            var result = await _caseService.UpdateCaseStatus(caseId, newStatus);

            if (!result.success ?? false)
                return NotFound(result.data);

            return Ok(result.data);
        }
    }
}
