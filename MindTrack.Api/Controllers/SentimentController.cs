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
        public async Task<ActionResult<SentimentAnalysisResultDTO>> Analyze([FromBody] SentimentData input) =>
         Ok(await _sentimentAnalysisService.AnalyzeSentiment(input.Text));

    }
}
