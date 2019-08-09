using System.Collections.Generic;
using System.Threading;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public interface IPredictionBuilder
    {
        List<PredictionNode> GetDepthwisePrediction(PredictionNode node, Team teamPlaying, int depth, CancellationToken token);
    }
}