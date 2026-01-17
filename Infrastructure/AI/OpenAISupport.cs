using Application;
using Application.Interface;
using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.AI
{
    public class OpenAISupport : IAISupport
    {
        private readonly IChatClient _chatClient;
        private readonly List<ChatMessage> _conversation = [];
        private readonly static string SYSTEM_DECRIPTION =
            """
            Bạn là một trợ lý dạy tiếng Anh chuyên nghiệp. 
            Luôn trả lời các câu hỏi về học tiếng Anh: ngữ pháp, từ vựng, phát âm, kỹ năng nghe/nói/đọc/viết.
            Nếu người dùng hỏi về chủ đề không liên quan đến học tiếng Anh, từ chối trả lời và nhắc họ giữ câu hỏi liên quan.
            Giải thích chi tiết, dễ hiểu, có ví dụ minh họa.
            Luôn giữ giọng điệu thân thiện, khích lệ học viên.
            """;
        public OpenAISupport(IChatClient chatClient)
        {
            _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
            _conversation.Add(new ChatMessage(ChatRole.System, SYSTEM_DECRIPTION));
        }
        public async Task<string> GetAIResponse(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return string.Empty;
            }
            _conversation.Add(new ChatMessage(ChatRole.User, prompt));
            var response = await _chatClient.GetResponseAsync(_conversation);
            return response.Text;
        }
    }
}
