using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Domain.Entities;

namespace MindTrack.Domain.Interfaces
{
    public interface ISentimentAnalysisService
    {
        SentimentAnalysisResultDTO AnalyzeSentiment(string text);
    }
}
