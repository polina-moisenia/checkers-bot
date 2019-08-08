using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IPossibleMovesCalc
    {
        List<List<Move>> GetPossibleMoves(CellState[,] board, Team teamPlaying);
    }
}
