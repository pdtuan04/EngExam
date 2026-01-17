using Domain.Entity;

namespace EngExam.Models
{
    public class QuestionModel
    {
        public required string Context { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string Explanation { get; set; }
    }
    public enum QuestionTypes
    {
        MultipleChoice,
        FillInTheBlank,
    }
}
