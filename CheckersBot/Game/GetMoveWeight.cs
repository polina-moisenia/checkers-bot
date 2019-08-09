using System.Collections.Generic;
using System.Linq;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class GetMoveWeight : IGetMoveWeight
    {
        public int CalculateMoveWeight(List<Move> moves, CellState[,] boardBefore, CellState[,] boardAfter, Team team, bool isBeat = false)
        {
            var weight = 0;
            var initStat = boardBefore.GetBoardStats();
            var endStat = boardAfter.GetBoardStats();

            if (isBeat)
            {
                if (boardAfter.CountEnemies(team) == 0)
                    return 100;
                weight += moves.Count * 5;

                if (initStat.BlackKings > endStat.BlackKings && team == Team.White)
                {
                    weight += (initStat.BlackKings - endStat.BlackKings) * 10;
                }

                if (initStat.WhiteKings > endStat.WhiteKings && team == Team.Black)
                {
                    weight += (initStat.WhiteKings - endStat.WhiteKings) * 15;
                }
            }

            if (initStat.BlackKings < endStat.BlackKings && team == Team.Black)
            {
                weight += (endStat.BlackKings - initStat.BlackKings) * 8;
            }

            if (initStat.WhiteKings < endStat.WhiteKings && team == Team.White)
            {
                weight += (endStat.WhiteKings - initStat.WhiteKings) * 8;
            }

            if (initStat.BlackKings > endStat.BlackKings && team == Team.Black)
            {
                weight -= (initStat.BlackKings - endStat.BlackKings) * 10;
            }

            if (initStat.WhiteKings > endStat.WhiteKings && team == Team.White)
            {
                weight -= (initStat.WhiteKings - endStat.WhiteKings) * 10;
            }

            var firstPosition = moves.First().StartingPoint;
            var endPosition = moves.Last().EndingPoint;

            if (team == Team.White)
            {
                if (firstPosition.Y > endPosition.Y)
                    weight+=2;
            }

            if (team == Team.Black)
            {
                if (firstPosition.Y < endPosition.Y)
                    weight+=2;
            }

            var enemies = team.GetEnemies();

            var increments = new List<int> { -1, 1};
            foreach (var xInc in increments)
            {
                foreach (var yInc in increments)
                {
                    if (firstPosition.X + xInc < 8 && firstPosition.X + xInc >= 0 &&
                        firstPosition.Y + yInc < 8 && firstPosition.Y + yInc >= 0 &&
                        enemies.Contains(boardBefore[firstPosition.X + xInc, firstPosition.Y + yInc]))
                    {
                        weight += 4;
                    }

                    if (endPosition.X + xInc < 8 && endPosition.X + xInc >= 0 &&
                        endPosition.Y + yInc < 8 && endPosition.Y + yInc >= 0 &&
                        enemies.Contains(boardAfter[endPosition.X + xInc, endPosition.Y + yInc]))
                    {
                        weight -= 4;
                    }
                }
            }

            if (firstPosition.X < endPosition.X && firstPosition.X < 2)
                weight--;

            if (firstPosition.X > endPosition.X && firstPosition.X < 2)
                weight++;

            if (firstPosition.X > endPosition.X && firstPosition.X > 5)
                weight--;

            if (firstPosition.X < endPosition.X && firstPosition.X > 5)
                weight++;

            return weight;
        }
    }
}
