using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class VocabularyHelper
    {
        public static string MaskWord(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            var wordLength = word.Length;
            var wordChars = word.ToCharArray();
            var shownIndexes = new HashSet<int>();
            var visibleChars = wordLength switch
            {
                1 => 1,
                2 or 3 => 1,
                <= 10 => 2,
                _ => 3
            };
            while(shownIndexes.Count < visibleChars)
            {
                shownIndexes.Add(Random.Shared.Next(0, wordLength));
            }
            var hiddenWord = new string('_', wordLength).ToCharArray();
            foreach (var index in shownIndexes)
            {
                hiddenWord[index] = wordChars[index];
            }
            return new string(hiddenWord);
        }
    }
}
