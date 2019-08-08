
using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Extensions
{
    public static class TeamExtensions
    {
        public static Team GetNextTeam(this Team team)
        {
            return team == Team.Black ? Team.White : Team.Black;
        }

        public static List<CellState> GetEnemies(this Team team)
        {
            if (team == Team.White)
            {
                return new List<CellState>{CellState.BlackKing, CellState.BlackPiece};
            }

            return new List<CellState> { CellState.WhiteKing, CellState.WhitePiece };
        }
    }
}
