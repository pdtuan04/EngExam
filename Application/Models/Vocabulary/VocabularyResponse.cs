using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Vocabulary
{
    public sealed record VocabularyResponse(
        Guid Id,
        string Word,
        string Phonetic,
        string Meaning,
        string PronunciationAudioUrl,
        PartOfSpeech PartOfSpeech);
}
