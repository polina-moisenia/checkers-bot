using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IGetMoveWeight
    {
         (int weight, int stats) CalculateMoveWeight(List<Move> moves, CellState[,] boardBefore, CellState[,] boardAfter, Team team, bool isBeat = false);
    }
}
