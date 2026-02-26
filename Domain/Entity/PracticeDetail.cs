namespace Domain.Entity
{
    public class PracticeDetail
    {
        public required Guid PracticeId { get; set; }
        public Practice Practice { get; set; } = null!;
        public required Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}