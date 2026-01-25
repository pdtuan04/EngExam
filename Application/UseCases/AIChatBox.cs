using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AIChatBox : IAIChatBox
    {
        public readonly IAISupport _aiSupport;
        
        public AIChatBox(IAISupport aiSupport)
        {
            _aiSupport = aiSupport ?? throw new ArgumentNullException(nameof(aiSupport));
        }
        public async Task<string> GetAIResponseAsync(string prompt)
        {
            var response = await _aiSupport.GetAIResponse(prompt);
            return response;
        }
    }
}
