using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class VocabularyHelper
    {
        public static string MaskWord(string word)
        {
            if (word == null)
                return string.Empty;
            int length = word.Length;
            if (length <= 2)
                return word; // No masking for very short words
            int maskLength = length - 2; // Keep first and last character
            string maskedPart = new string('*', maskLength);
            return $"{word[0]}{maskedPart}{word[length - 1]}";
        }
    }
}
