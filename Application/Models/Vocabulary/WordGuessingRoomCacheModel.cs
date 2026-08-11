using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Vocabulary
{
    public sealed record WordGuessingRoomCacheModel(
        Guid Id,
        string RoomCode,
        Guid? Player1UserId,
        Guid? Player2UserId,
        string? Player1ConnectionId,
        string? Player2ConnectionId,
        string? Player1Name,
        string? Player2Name,
        int Player1Score,
        int Player2Score,
        List<VocabularyResponse> Words,
        int CurrentWordIndex,
        WordGuessingStatus Status,
        long Version);
}
