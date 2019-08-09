using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class NextMovesCalculator : INextMovesCalculator
    {
        private readonly Random _random = new Random();
        private readonly IPredictionBuilder _predictionBuilder;
        private readonly IGetMoveWeight _getMoveWeight;

        public NextMovesCalculator(IPredictionBuilder predictionBuilder, IGetMoveWeight getMoveWeight)
        {
            _predictionBuilder = predictionBuilder;
            _getMoveWeight = getMoveWeight;
        }

        public List<Move> GetCalculatedNextMoves(CellState[,] board, Team team, List<List<Move>> beats, List<List<Move>> moves, CancellationToken token)
        {
            var predictions = new List<PredictionNode>();

            foreach (var beat in beats)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                var updatedBoard = board.UpdateFromMoves(beat);

                if (updatedBoard.CountEnemies(team) == 0)
                    return beat;

                var node = new PredictionNode
                {
                    InitialMoves = beat,
                    NextTeam = team.GetNextTeam(),
                    NextBoard = updatedBoard,
                    Depth = 0,
                    AccumulatedWeight = _getMoveWeight.CalculateMoveWeight(beat, board, updatedBoard, team, true)
                };

                predictions.AddRange(_predictionBuilder.GetDepthwisePrediction(node, team, 3, token));
            }

            foreach (var move in moves)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                var updatedBoard = board.UpdateFromMoves(move);

                var node = new PredictionNode
                {
                    InitialMoves = move,
                    NextTeam = team.GetNextTeam(),
                    NextBoard = updatedBoard,
                    Depth = 0,
                    AccumulatedWeight = _getMoveWeight.CalculateMoveWeight(move, board, updatedBoard, team)
                };

                predictions.AddRange(_predictionBuilder.GetDepthwisePrediction(node, team, 3, token));
            }

            var count = predictions.Count;
            var topBeats = predictions.OrderByDescending(r => r.AccumulatedWeight).Take(5).Select(r => r.InitialMoves).ToList();
            var countTop = topBeats.Count < count ? topBeats.Count : count;
            return topBeats[_random.Next(0, countTop - 1)];
        }
    }
}
