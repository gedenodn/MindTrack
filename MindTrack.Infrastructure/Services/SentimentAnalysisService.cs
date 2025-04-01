using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using MindTrack.Domain.Entities;
using MindTrack.Domain.Interfaces;
using MindTrack.Infrastructure.ML;

namespace MindTrack.Infrastructure.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentAnalysisService(string modelPath)
        {
            _mlContext = new MLContext();
            _model = _mlContext.Model.Load(modelPath, out var modelInputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }

        public SentimentAnalysisResultDTO AnalyzeSentiment(string text)
        {
            var prediction = _predictionEngine.Predict(new SentimentData { Text = text });

            var sentiment = prediction.Score > 0.5f ? "Positive" : "Negative"; 
            return new SentimentAnalysisResultDTO
            {
                Sentiment = sentiment,
                Score = prediction.Score
            };
        }
    }
}
