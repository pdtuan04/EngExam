using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Question;
using System;

namespace Application.Features.Exam.Events
{
    public sealed record CreateExamEvent(
        ExamReadModel Exam,
        List<QuestionReadModel> Questions,
        List<AnswerReadModel> Answers,
        List<ExamDetailReadModel> ExamDetails
    );
}