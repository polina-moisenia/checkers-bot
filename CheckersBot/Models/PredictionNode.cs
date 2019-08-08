using System.Collections.Generic;
using CheckersBot.Extensions;

namespace CheckersBot.Models
{
    public class PredictionNode
    {
        public readonly List<Move> CurrentMoves;
        public readonly Team CurrentTeam;
        public readonly CellState[,] UpdatedBoard;
        public readonly int CurrentDepth;
        public PredictionNode InitialParent { get; private set; }

        public PredictionNode(PredictionNode parent, List<Move> move, Team team, CellState[,] board, int depth)
        {
            InitialParent = parent;
            CurrentMoves = move;
            CurrentTeam = team;
            UpdatedBoard = board.UpdateFromMoves(move);
            CurrentDepth = depth;
        }
    }
}
