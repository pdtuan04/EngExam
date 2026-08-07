using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Vocabulary
{
    public sealed record HiddenVocabularyResponse(
        Guid Id,
        string HiddenWord,
        string Phonetic,
        string Meaning,
        string PronunciationAudioUrl, 
        PartOfSpeech PartOfSpeech);
}
