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
        private readonly IRankMoves _rankMoves;
        private readonly IPredictionBuilder _predictionBuilder;

        public NextMovesCalculator(IRankMoves rankMoves, IPredictionBuilder predictionBuilder)
        {
            _rankMoves = rankMoves;
            _predictionBuilder = predictionBuilder;
        }

        public List<Move> GetCalculatedNextMoves(CellState[,] board, Team team, List<List<Move>> beats, List<List<Move>> moves, CancellationToken token)
        {
            var stats = board.GetBoardStats();
            if (stats.BlackPieces > 8 && stats.WhitePieces > 8)
            {
                if (beats.Count > 0)
                {
                    var rankedBeats = _rankMoves.GetRanks(beats, team, board);
                    return GetRandomFromTopThree(rankedBeats);
                }

                var rankedMoves = _rankMoves.GetRanks(moves, team, board);
                return GetRandomFromTopThree(rankedMoves);
            }

            var currentOptions = new List<List<Move>>();
            currentOptions.AddRange(beats);
            currentOptions.AddRange(moves);

            var predictions = new List<PredictionNode>();

            foreach (var move in currentOptions)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();
                var node = new PredictionNode(null, move, team, board, 0);
                predictions.AddRange(_predictionBuilder.GetDepthwisePrediction(node,3, token));
            }

            var ranks = new List<MoveRank>();
            foreach (var prediction in predictions)
            {
                var rank = new MoveRank
                {
                    Rank = GetRankFromBoard(team, prediction.UpdatedBoard.GetBoardStats()),
                    Move = prediction.InitialParent.CurrentMoves
                };

                ranks.Add(rank);
            }

            return GetRandomFromTopThree(ranks);
        }

        private int GetRankFromBoard(Team team, GameStats stats)
        {
            var blackStats = stats.BlackPieces + stats.BlackKings * 3;
            var whiteStats = stats.WhitePieces + stats.WhiteKings * 3;
            var rank = blackStats - whiteStats;

            return team == Team.Black ? rank : -rank;
        }

        private List<Move> GetRandomFromTopThree(List<MoveRank> ranks)
        {
            var topBeats = ranks.OrderByDescending(r => r.Rank).Take(3).Select(r => r.Move).ToList();
            var count = topBeats.Count < ranks.Count ? topBeats.Count : ranks.Count;
            return topBeats[_random.Next(0, count - 1)];
        }
    }
}
