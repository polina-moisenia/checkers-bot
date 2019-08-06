using System;
using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class NextMovesChooser : INextMovesChooser
    {
        private readonly Random _random = new Random();
        private readonly IPossibleBeatsCalc _beatsCalc;
        private readonly IPossibleMovesCalc _movesCalc;

        public NextMovesChooser(IPossibleBeatsCalc beatsCalc, IPossibleMovesCalc movesCalc)
        {
            _beatsCalc = beatsCalc;
            _movesCalc = movesCalc;
        }

        public List<Move> GetNextMove(BoardModel currentBoard)
        {
            var boardArray = currentBoard.ConvertToArray();
            var team = currentBoard.TeamToMoveNext;

            var beats = _beatsCalc.GetPossibleBeats(boardArray, team);

            if (beats.Count > 0)
            {
                //TODO add some strategy for choosing the beat :
                // - beat as munch as one can
                // - not allow kill yourself without a reason
                // - try to get to kings
                return beats[_random.Next(0, beats.Count - 1)];
            }

            var possibleMoves = _movesCalc.GetPossibleMoves(boardArray, team);
            //TODO add some strategy for choosing the move :
            // - not go under the beat without good reason
            // - not allow kill yourself without good reason
            // - try to get to kings
            // - sacrifice the ordinary piece, not king
            // - prefer not going into corner
            return new List<Move> {possibleMoves[_random.Next(0, possibleMoves.Count - 1)]};
        }
    }
}
