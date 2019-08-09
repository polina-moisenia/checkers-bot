using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class PredictionBuilder : IPredictionBuilder
    {
        private readonly IPossibleBeatsCalc _beatsCalc;
        private readonly IPossibleMovesCalc _movesCalc;
        private readonly IGetMoveWeight _getMoveWeight;

        public PredictionBuilder(IPossibleBeatsCalc beatsCalc, IPossibleMovesCalc movesCalc, IGetMoveWeight getMoveWeight)
        {
            _beatsCalc = beatsCalc;
            _movesCalc = movesCalc;
            _getMoveWeight = getMoveWeight;
        }

        public List<PredictionNode> GetDepthwisePrediction(PredictionNode node, Team teamPlaying, int predictionDepth, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            var teamNext = node.NextTeam;
            var boardNext = node.NextBoard;

            var toKill = boardNext.CountEnemies(teamNext);
            var depth = node.Depth + 1;
            var beats = _beatsCalc.GetPossibleBeats(boardNext, teamNext);

            var nextPossibilities = new List<PredictionNode>();

            foreach (var beat in beats)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                if (beat.Count > 0)
                {
                    var updatedBoard = boardNext.UpdateFromMoves(beat);
                    var rank = _getMoveWeight.CalculateMoveWeight(beat, boardNext, updatedBoard, teamNext, true);

                    if (rank.stats > -2) {

                        var nodeNext = new PredictionNode
                        {
                            InitialMoves = node.InitialMoves,
                            NextTeam = teamNext.GetNextTeam(),
                            NextBoard = updatedBoard,
                            Depth = depth,
                            AccumulatedWeight = node.AccumulatedWeight + (teamNext == teamPlaying ? +rank.weight : 0),
                            StatsForPlayer = teamNext == teamPlaying ? +rank.weight : -rank.weight
                        };

                        if (depth == predictionDepth || toKill == 0)
                            nextPossibilities.Add(nodeNext);

                        if (depth < predictionDepth && toKill > 0)
                            nextPossibilities.AddRange(GetDepthwisePrediction(nodeNext, teamPlaying, predictionDepth,
                                token));
                    }
                }
            }

            var moves = _movesCalc.GetPossibleMoves(boardNext, teamNext);
            foreach (var move in moves)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                var updatedBoard = boardNext.UpdateFromMoves(move);
                var rank = _getMoveWeight.CalculateMoveWeight(move, boardNext, updatedBoard, teamNext);

                var nodeNext = new PredictionNode
                {
                    InitialMoves = node.InitialMoves,
                    NextTeam = teamNext.GetNextTeam(),
                    NextBoard = updatedBoard,
                    Depth = depth,
                    AccumulatedWeight = node.AccumulatedWeight + (teamNext == teamPlaying ? +rank.weight : -rank.weight),
                    StatsForPlayer = teamNext == teamPlaying ? +rank.weight : -rank.weight
                };

                if (depth == predictionDepth)
                    nextPossibilities.Add(nodeNext);
                if (depth < predictionDepth)
                    nextPossibilities.AddRange(GetDepthwisePrediction(nodeNext, teamPlaying, predictionDepth, token));
            }

            return nextPossibilities;
        }
    }
}
