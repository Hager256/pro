using Microsoft.AspNetCore.Mvc;
using MotivAi.Models;
using MotivAi.Services;

namespace MotivAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoadmapController : ControllerBase
    {
        private readonly RoadmapService _roadmapService;

        public RoadmapController(RoadmapService roadmapService)
        {
            _roadmapService = roadmapService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] RoadmapRequest request)
        {
            try
            {
                var roadmap = await _roadmapService.GenerateAsync(request);
                return Ok(roadmap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}