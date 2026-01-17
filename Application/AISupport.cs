using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AISupport: IAISupport
    {
        public Task<string> GetAIResponse(string prompt)
        {
            // Implement your AI response logic here
            return Task.FromResult($"AI response to the prompt: {prompt}");
        }
    }
}
