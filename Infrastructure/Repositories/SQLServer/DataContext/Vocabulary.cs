using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Vocabulary : BaseEntity<Guid>, ISoftDeletable
    {
        public required string Word { get; set; }
        public required string Phonetic { get; set; }
        public required string Meaning { get; set; }
        public required string PronunciationAudioUrl { get; set; }
        public required PartOfSpeech PartOfSpeech { get; set; }
        public bool IsDeleted { get; set; }
    }
}
