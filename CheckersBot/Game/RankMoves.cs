using System.Collections.Generic;
using System.Linq;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class RankMoves : IRankMoves
    {
        public List<MoveRank> GetRanks(List<List<Move>> moves, Team team, CellState[,] board)
        {
            var ranks = new List<MoveRank>();

            foreach (var move in moves)
            {
                var enemies = team.GetEnemies();

                var updatedBoard = board.UpdateFromMoves(move);

                var firstPosition = move.First().StartingPoint;
                var endPosition = move.Last().EndingPoint;

                var rank = 0;

                rank += move.Count;

                if (firstPosition.X + 1 < 8 && firstPosition.Y + 1 < 8 && enemies.Contains(updatedBoard[firstPosition.X + 1, firstPosition.Y + 1]) ||
                    firstPosition.X + 1 < 8 && firstPosition.Y - 1 >= 0 && enemies.Contains(updatedBoard[firstPosition.X + 1, firstPosition.Y - 1]) ||
                    firstPosition.X - 1 >= 0 && firstPosition.Y - 1 >= 0 && enemies.Contains(updatedBoard[firstPosition.X - 1, firstPosition.Y - 1]) ||
                    firstPosition.X - 1 >= 0 && firstPosition.Y + 1 < 8 && enemies.Contains(updatedBoard[firstPosition.X - 1, firstPosition.Y + 1]))
                {
                    rank += 2;
                }

                if (endPosition.X + 1 < 8 && endPosition.Y + 1 < 8 && enemies.Contains(updatedBoard[endPosition.X + 1, endPosition.Y + 1]) ||
                    endPosition.X + 1 < 8 && endPosition.Y - 1 >= 0 && enemies.Contains(updatedBoard[endPosition.X + 1, endPosition.Y - 1]) ||
                    endPosition.X - 1 >= 0 && endPosition.Y - 1 >= 0 && enemies.Contains(updatedBoard[endPosition.X - 1, endPosition.Y - 1]) ||
                    endPosition.X - 1 >= 0 && endPosition.Y + 1 < 8 && enemies.Contains(updatedBoard[endPosition.X - 1, endPosition.Y + 1]))
                {
                    rank--;
                }

                if (team == Team.White)
                {
                    if (firstPosition.Y > endPosition.Y)
                        rank++;
                }

                if (team == Team.Black)
                {
                    if (firstPosition.Y < endPosition.Y)
                        rank++;
                }

                if (firstPosition.X < endPosition.X && firstPosition.X < 5)
                    rank++;

                if (firstPosition.X > endPosition.X && firstPosition.X > 5)
                    rank++;

                ranks.Add(new MoveRank { Rank = rank, Move = move});
            }

            return ranks;
        }
    }
}
