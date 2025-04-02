using Microsoft.AspNetCore.Mvc;
using Serilog;
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
                Log.Warning("Received empty request for sentiment analysis");
                return BadRequest("Text cannot be empty.");
            }

            Log.Information($"Received sentiment analysis request for text: {input.Text}");
            var result = _sentimentAnalysisService.AnalyzeSentiment(input.Text);

            Log.Information($"Analysis completed. Result: {result.Sentiment}");
            return Ok(result);
        }
    }
}
