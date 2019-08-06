using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IPossibleMovesCalc
    {
        List<Move> GetPossibleMoves(CellState[,] board, Team teamPlaying);
    }
}
