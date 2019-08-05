using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Services
{
    public interface IMove
    {
        List<Move> GetNextMove(BoardModel currentBoard);
    }
}
