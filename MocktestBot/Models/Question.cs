namespace MocktestBot.Models
{
    public class Question
    {
        public required string Type { get; set; } // MCQ, ONE_WORD, etc.
        public required string QuestionString { get; set; }
        public List<string>? Options { get; set; } // Only for MCQ type
        public required string ActualAnswer { get; set; }
        public string? UsersAnswer { get; set; }
        public required string Evaluation { get; set; } // CORRECT, INCORRECT, UNANSWERED
    }
}
