namespace MocktestBot.Models
{
    public class ContinueGeneratingRequest
    {
        public required string Topic { get; set; }
        public required List<Question> PreviousQuestions { get; set; }
    }
}
