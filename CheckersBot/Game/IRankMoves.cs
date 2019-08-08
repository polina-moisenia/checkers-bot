using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IRankMoves
    {
        List<MoveRank> GetRanks(List<List<Move>> moves, Team team, CellState[,] board);
    }
}
