using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Infrastructure.ML;

namespace MindTrack.Infrastructure.Services
{
    public static class SentimentDataLoader
    {
        public static List<SentimentData> LoadData(string dataDirectory)
        {
            var data = new List<SentimentData>();

            string trainPosPath = Path.Combine(dataDirectory, "train", "pos");
            string trainNegPath = Path.Combine(dataDirectory, "train", "neg");

            if (Directory.Exists(trainPosPath))
            {
                data.AddRange(Directory.GetFiles(trainPosPath).Select(file => new SentimentData
                {
                    Text = File.ReadAllText(file),
                    Sentiment = true
                }));
            }

            if (Directory.Exists(trainNegPath))
            {
                data.AddRange(Directory.GetFiles(trainNegPath).Select(file => new SentimentData
                {
                    Text = File.ReadAllText(file),
                    Sentiment = false
                }));
            }

            return data;
        }
    }

}
