namespace Flashcards.DTOs
{
    public class CreateFlashcardDTO
    {
        public int CardStackId { get; set; }
        public string Front {  get; set; }
        public string Back { get; set; }
    }
}
