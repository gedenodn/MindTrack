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
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentAnalysisService(string modelPath)
        {
            _mlContext = new MLContext();
            var model = _mlContext.Model.Load(modelPath, out var _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        public SentimentAnalysisResultDTO AnalyzeSentiment(string text)
        {
            var prediction = _predictionEngine.Predict(new SentimentData { Text = text });

            return new SentimentAnalysisResultDTO
            {
                Sentiment = prediction.Sentiment ? "Positive" : "Negative",
                Score = prediction.Score
            };
        }
    }
}
