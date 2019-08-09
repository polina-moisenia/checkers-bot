using System.Collections.Generic;

namespace CheckersBot.Models
{
    public class PredictionNode
    {
        public int Depth { get; set; }
        public int AccumulatedWeight { get; set; }
        public List<Move> InitialMoves { get; set; }
        public Team NextTeam { get; set; }
        public CellState[,] NextBoard { get; set; }
    }
}
