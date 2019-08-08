using System.Collections.Generic;
using System.Threading.Tasks;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IGetNextMoves
    {
        Task<List<Move>> GetMovesBeforeTimeoutAsync(BoardModel board);
    }
}
