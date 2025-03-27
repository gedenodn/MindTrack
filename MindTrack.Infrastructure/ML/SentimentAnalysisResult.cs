using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Infrastructure.ML
{
    public class SentimentAnalysisResult
    {
        public string Sentiment { get; set; }
        public float Score { get; set; }
    }
}
