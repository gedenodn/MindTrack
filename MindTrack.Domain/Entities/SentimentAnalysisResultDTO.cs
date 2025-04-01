using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Domain.Entities
{
    public class SentimentAnalysisResultDTO
    {
        public string Sentiment { get; set; }
        public float Score { get; set; }
    }
}
