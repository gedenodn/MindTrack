using Microsoft.ML;
using Serilog;
using MindTrack.Domain.Entities;
using MindTrack.Domain.Interfaces;
using MindTrack.Infrastructure.ML;

namespace MindTrack.Infrastructure.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentAnalysisService(string modelPath)
        {
            _mlContext = new MLContext();

            try
            {
                var model = _mlContext.Model.Load(modelPath, out var _);
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
                Log.Information("Model loaded successfully");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to load model: {ex.Message}");
                throw;
            }
        }

        public SentimentAnalysisResultDTO AnalyzeSentiment(string text)
        {
            Log.Information($"Analyzing text: {text}");

            try
            {
                var prediction = _predictionEngine.Predict(new SentimentData { Text = text });

                var result = new SentimentAnalysisResultDTO
                {
                    Sentiment = prediction.Sentiment ? "Positive" : "Negative",
                    Score = prediction.Score
                };

                Log.Information($"Sentiment Analysis Result: {result.Sentiment}, Score: {result.Score:F4}");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Sentiment analysis failed: {ex.Message}");
                throw;
            }
        }
    }
}
