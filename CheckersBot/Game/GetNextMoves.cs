using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class GetNextMoves : IGetNextMoves
    {
        private readonly Random _random = new Random();
        private readonly IPossibleBeatsCalc _beatsCalc;
        private readonly IPossibleMovesCalc _movesCalc;
        private readonly INextMovesCalculator _nextMovesCalculator;

        public GetNextMoves(IPossibleBeatsCalc beatsCalc, IPossibleMovesCalc movesCalc, INextMovesCalculator nextMovesCalculator)
        {
            _beatsCalc = beatsCalc;
            _movesCalc = movesCalc;
            _nextMovesCalculator = nextMovesCalculator;
        }

        public async Task<List<Move>> GetMovesBeforeTimeoutAsync(BoardModel boardModel)
        {
            var board = boardModel.ConvertToArray();
            var team = boardModel.TeamToMoveNext;

            var beats = _beatsCalc.GetPossibleBeats(board, team);
            var moves = _movesCalc.GetPossibleMoves(board, team);

            //TODO comment out if won't work
            using (var cancellationTokenSource = new CancellationTokenSource(4800))
            {
                try
                {
                    var calcNextMoves = _nextMovesCalculator.GetCalculatedNextMoves(board, team, beats, moves, cancellationTokenSource.Token);
                    return calcNextMoves;
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Task was cancelled");
                }
            }

            if (beats.Count > 0)
            {
                return beats[_random.Next(0, beats.Count - 1)];
            }
            return moves[_random.Next(0, moves.Count - 1)];
        }
    }
}
