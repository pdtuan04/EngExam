using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Translate
{
    public class GASTranslate : ITranslateService
    {
        private readonly HttpClient _httpClient;
        public GASTranslate(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<string>> TranslateAsync(string Text)
        {
            throw new NotImplementedException();
        }
    }
}
