using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Caching
{
    public static class CacheKeys
    {
        // Category 
        public static string AllExamCategories = "examCategory:all";
        public static string ExamCategoryDetail(Guid id) => $"examCategory:detail:{id}";

        //Comment
        public static string CourseComments(Guid courseId) => $"course:comments:{courseId}";

        // Course
        public static string CourseDetail(Guid id) => $"course:detail:{id}";

        // Exam
        public static string ExamByCategory(Guid categoryId) => $"exam:category:{categoryId}";
        public static string ExamDetail(Guid id) => $"exam:detail:{id}";
        public static string ExamToTake(Guid id) => $"exam:take:{id}";
        public static string ExamSuggested(string name) => $"exam:suggested:{name}";

        // Exam Result
        public static string ExamResultDetail(Guid id) => $"examResult:detail:{id}";

        // Practice
        public static string PracticeToTake(Guid id) => $"practice:take:{id}";
        public static string PracticeDetails(Guid id) => $"practice:detail:{id}";

        //Topic
        public static string AllTopics = "topic:all";
        public static string TopicDetail(Guid id) => $"topic:{id}";

        //User
        public static string UserDetail(Guid id) => $"user:detail:{id}";
        public static string OnlineUsers => "all:online";
        //Auth
        public static string JwtToken(string token) => $"jwt:{token}";

        //Word
        public static string WordMeaning(string word) => $"word:meanings:{word}";
        //FlashCard
        public static string FlashCardsByUser(Guid userId) => $"flashCards:user:{userId}";
        public static string FlashCardDetail(Guid id) => $"flashCard:detail:{id}";

        //Waiting Rooms
        public static string WaitingRooms => "waiting:rooms";
        public static string GuessingRoom(string code) => $"guessingroom:room:{code}";
    }
}
