namespace MindTrack.Domain.Entities
{
    public class MoodEntry
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Sentiment { get; set; }
        public float Score { get; set; }
    }
}
