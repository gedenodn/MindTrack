namespace MindTrack.Domain.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string SentimentType { get; set; }
        public string AdviceText { get; set; }
        public int MoodEntryId { get; set; }
        public MoodEntry MoodEntry { get; set; }
    }
}