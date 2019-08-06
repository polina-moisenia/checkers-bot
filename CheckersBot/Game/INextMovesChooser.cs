using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface INextMovesChooser
    {
        List<Move> GetNextMove(BoardModel currentBoard);
    }
}
