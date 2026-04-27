using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITranslateService
    {
        Task<IEnumerable<string>> TranslateAsync(string Text);
    }
}
