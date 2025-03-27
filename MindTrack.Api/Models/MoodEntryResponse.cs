namespace MindTrack.Api.Models
{
    public class MoodEntryResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Sentiment { get; set; }
        public float Score { get; set; }
    }
}
