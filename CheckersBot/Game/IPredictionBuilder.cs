using System.Collections.Generic;
using System.Threading;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IPredictionBuilder
    {
        List<PredictionNode> GetDepthwisePrediction(PredictionNode node, int depth, CancellationToken token);
    }
}