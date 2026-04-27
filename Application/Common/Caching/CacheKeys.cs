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

        // Course
        public static string CourseDetail(Guid id) => $"course:detail:{id}";

        // Exam
        public static string ExamByCategory(Guid categoryId) => $"exam:category:{categoryId}";
        public static string ExamDetail(Guid id) => $"exam:detail:{id}";
        public static string ExamToTake(Guid id) => $"exam:take:{id}";

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
        //Auth
        public static string JwtToken(string token) => $"jwt:{token}";

        //Word
        public static string WordMeaning(string word) => $"word:meaning:{word}";
    }
}
