using Application.Common.Helpers;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Vocabulary
{
    public sealed record HiddenVocabularyResponse
    {
        public Guid Id { get; init; }
        public string HiddenWord { get; init; }
        public string Phonetic { get; init; }
        public string Meaning { get; init; }
        public string? PronunciationAudioUrl { get; init; }
        public PartOfSpeech PartOfSpeech { get; init; }
        public HiddenVocabularyResponse(Guid id, string hiddenWord, string phonetic, string meaning, string? pronunciationAudioUrl, PartOfSpeech partOfSpeech)
        {
            Id = id;
            HiddenWord = hiddenWord;
            Phonetic = phonetic;
            Meaning = meaning;
            PronunciationAudioUrl = string.IsNullOrEmpty(pronunciationAudioUrl) ? null : pronunciationAudioUrl.GetFileUrl();
            PartOfSpeech = partOfSpeech;
        }
    }
}
