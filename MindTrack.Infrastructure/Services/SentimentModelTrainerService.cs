using Microsoft.ML;
using Serilog;
using MindTrack.Infrastructure.ML;

namespace MindTrack.Infrastructure.Services
{
    public class SentimentModelTrainer
    {
        public static async Task TrainAndSaveModelAsync(string dataDirectory, string modelPath)
        {
            var mlContext = new MLContext();
            Log.Information("MLContext initialized");

            try
            {
                Log.Information("Loading training data...");
                List<SentimentData> trainingData = await LoadDataAsync(dataDirectory);
                Log.Information($"Training data loaded: {trainingData.Count} samples");

                var dataView = mlContext.Data.LoadFromEnumerable(trainingData);
                var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
                var trainData = trainTestSplit.TrainSet;
                var testData = trainTestSplit.TestSet;
                Log.Information("Data split into training and test sets");

                var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                    .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Sentiment", featureColumnName: "Features"));

                Log.Information("Training model...");
                var model = pipeline.Fit(trainData);
                Log.Information("Model training completed");

                var predictions = model.Transform(testData);
                var metrics = mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "Sentiment");

                Log.Information($"Model Accuracy: {metrics.Accuracy:P2}");
                Log.Information($"F1 Score: {metrics.F1Score:P2}");
                Log.Information($"Log Loss: {metrics.LogLoss:F4}");

                mlContext.Model.Save(model, trainData.Schema, modelPath);
                Log.Information($"Model saved at: {modelPath}");
            }
            catch (Exception ex)
            {
                Log.Error($"Model training failed: {ex.Message}");
            }
        }

        private static async Task<List<SentimentData>> LoadDataAsync(string dataDirectory)
        {
            var data = new List<SentimentData>();

            try
            {
                string trainPosPath = Path.Combine(dataDirectory, "train", "pos");
                string trainNegPath = Path.Combine(dataDirectory, "train", "neg");

                if (Directory.Exists(trainPosPath))
                {
                    var posFiles = Directory.GetFiles(trainPosPath);
                    var posData = await Task.WhenAll(posFiles.Select(async file => new SentimentData
                    {
                        Text = await File.ReadAllTextAsync(file),
                        Sentiment = true
                    }));

                    data.AddRange(posData);
                    Log.Debug($"Loaded {posData.Length} positive samples");
                }

                if (Directory.Exists(trainNegPath))
                {
                    var negFiles = Directory.GetFiles(trainNegPath);
                    var negData = await Task.WhenAll(negFiles.Select(async file => new SentimentData
                    {
                        Text = await File.ReadAllTextAsync(file),
                        Sentiment = false
                    }));

                    data.AddRange(negData);
                    Log.Debug($"Loaded {negData.Length} negative samples");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error loading training data: {ex.Message}");
            }

            return data;
        }
    }
}
