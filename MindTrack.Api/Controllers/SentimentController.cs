using Microsoft.AspNetCore.Mvc;
using MindTrack.Domain.Interfaces;
using MindTrack.Domain.Entities;
using MindTrack.Infrastructure.ML;

namespace MindTrack.API.Controllers
{
    [ApiController]
    [Route("api/sentiment")]
    public class SentimentController : ControllerBase
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public SentimentController(ISentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        [HttpPost("analyze")]
        public ActionResult<SentimentAnalysisResultDTO> Analyze([FromBody] SentimentData input)
        {
            if (string.IsNullOrWhiteSpace(input.Text))
            {
                return BadRequest("Text cannot be empty.");
            }

            var result = _sentimentAnalysisService.AnalyzeSentiment(input.Text);
            return Ok(result);
        }
    }
}
