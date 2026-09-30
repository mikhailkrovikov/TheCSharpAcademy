namespace Flashcards.Entities
{
    public class StudySession
    {
        public int Id { get; set; }
        public int CardStackId { get; set; }
        public DateTime Time { get; set; }
        public int Score { get; set; }
    }
}
