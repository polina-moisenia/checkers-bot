using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IPossibleBeatsCalc
    {
        List<List<Move>> GetPossibleBeats(CellState[,] board, Team teamPlaying);
    }
}
