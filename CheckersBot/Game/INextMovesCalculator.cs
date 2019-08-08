using System.Collections.Generic;
using System.Threading;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface INextMovesCalculator
    {
        List<Move> GetCalculatedNextMoves(CellState[,] board, Team team, List<List<Move>> beats, List<List<Move>> moves, CancellationToken token);
    }
}
