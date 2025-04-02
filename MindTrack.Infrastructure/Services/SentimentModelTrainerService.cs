using Microsoft.ML;
using MindTrack.Infrastructure.ML;

namespace MindTrack.Infrastructure.Services
{
    public class SentimentModelTrainer
    {
        public static async Task TrainAndSaveModelAsync(string dataDirectory, string modelPath)
        {
            var mlContext = new MLContext();

            List<SentimentData> trainingData = await LoadDataAsync(dataDirectory);
            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainData = trainTestSplit.TrainSet;
            var testData = trainTestSplit.TestSet;

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Sentiment", featureColumnName: "Features"));

            var model = await Task.Run(() => pipeline.Fit(trainData));

            var predictions = model.Transform(testData);
            var metrics = mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "Sentiment");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");

            mlContext.Model.Save(model, trainData.Schema, modelPath);
            Console.WriteLine($"Model saved to: {modelPath}");
        }

        private static async Task<List<SentimentData>> LoadDataAsync(string dataDirectory)
        {
            var data = new List<SentimentData>();

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
            }

            return data;
        }
    }
}
