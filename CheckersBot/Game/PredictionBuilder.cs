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

        public PredictionBuilder(IPossibleBeatsCalc beatsCalc, IPossibleMovesCalc movesCalc)
        {
            _beatsCalc = beatsCalc;
            _movesCalc = movesCalc;
        }

        public List<PredictionNode> GetDepthwisePrediction(PredictionNode node, int predictionDepth, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            var nextPossibilities = new List<PredictionNode>();
            var depth = node.CurrentDepth + 1;

            var teamNext = node.CurrentTeam.GetNextTeam();
            var boardNext = node.UpdatedBoard;
            var beats = _beatsCalc.GetPossibleBeats(boardNext, teamNext);
            var moves = _movesCalc.GetPossibleMoves(boardNext, teamNext);
            var parent = node.InitialParent ?? node;

            foreach (var beat in beats)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();
                var nodeNext = new PredictionNode(parent, beat, teamNext, boardNext, depth);
                if (depth == predictionDepth)
                    nextPossibilities.Add(nodeNext);
                if (depth < predictionDepth)
                    nextPossibilities.AddRange(GetDepthwisePrediction(nodeNext, predictionDepth, token));
            }

            foreach (var move in moves)
            {
                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();
                var nodeNext = new PredictionNode(parent, move, teamNext, boardNext, depth);
                if (depth == predictionDepth)
                    nextPossibilities.Add(nodeNext);
                if (depth < predictionDepth)
                    nextPossibilities.AddRange(GetDepthwisePrediction(nodeNext, predictionDepth, token));
            }

            return nextPossibilities;
        }
    }
}
