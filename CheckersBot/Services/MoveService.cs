using System;
using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Services
{
    public class MoveService : IMove
    {
        public List<Move> GetNextMove(BoardModel currentBoard)
        {
            var beats = GetPossibleBeats(currentBoard);

            if (beats.Count > 0)
            {
                return beats[0];
            }
            else
            {
                return GetPossibleMoves(currentBoard)[0];
            }

        }
        public List<List<Move>> GetPossibleBeats(BoardModel currentBoard)
        {
            throw new NotImplementedException();
        }

        public List<List<Move>> GetPossibleMoves(BoardModel currentBoard)
        {
            throw new NotImplementedException();
        }
    }
}
