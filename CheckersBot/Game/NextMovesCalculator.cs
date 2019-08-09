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
            var firstPredictions = new List<PredictionNode>();

            try
            {
                var predictions = new List<PredictionNode>();
                foreach (var beat in beats)
                {
                    if (beat.Count > 0)
                    {
                        var updatedBoard = board.UpdateFromMoves(beat);

                        if (updatedBoard.CountEnemies(team) == 0)
                            return beat;

                        var ranking = _getMoveWeight.CalculateMoveWeight(beat, board, updatedBoard, team, true);
                        var node = new PredictionNode
                        {
                            InitialMoves = beat,
                            NextTeam = team.GetNextTeam(),
                            NextBoard = updatedBoard,
                            Depth = 0,
                            AccumulatedWeight = ranking.weight,
                            StatsForPlayer = ranking.stats
                        };

                        firstPredictions.Add(node);
                        predictions.AddRange(_predictionBuilder.GetDepthwisePrediction(node, team, 2, token));
                    }
                }

                foreach (var move in moves)
                {
                    if (token.IsCancellationRequested)
                        throw new TaskCanceledException();

                    var updatedBoard = board.UpdateFromMoves(move);
                    var ranking = _getMoveWeight.CalculateMoveWeight(move, board, updatedBoard, team);

                    var node = new PredictionNode
                    {
                        InitialMoves = move,
                        NextTeam = team.GetNextTeam(),
                        NextBoard = updatedBoard,
                        Depth = 0,
                        AccumulatedWeight = ranking.weight,
                        StatsForPlayer = ranking.stats
                    };

                    firstPredictions.Add(node);
                    predictions.AddRange(_predictionBuilder.GetDepthwisePrediction(node, team, 2, token));
                }

                return GetBestForRandom(predictions);
            }

            catch (TaskCanceledException)
            {
                Console.WriteLine("Task cancelled");
            }

            return GetBestForRandom(firstPredictions);
        }

        private List<Move> GetBestForRandom(List<PredictionNode> predictions)
        {
            //var topStat = predictions.Max(p => p.StatsForPlayer);
            //var topPredictionsByStat = predictions.FindAll(p => p.StatsForPlayer == topStat);

            var topWeight = predictions.Max(p => p.AccumulatedWeight);
            var topPredictionsByWeight = predictions.FindAll(p => (topWeight - p.AccumulatedWeight) <= 2);

            return topPredictionsByWeight[_random.Next(0, topPredictionsByWeight.Count - 1)].InitialMoves;
        }
    }
}
