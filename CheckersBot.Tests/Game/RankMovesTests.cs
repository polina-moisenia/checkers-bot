using System.Collections.Generic;
using System.Linq;
using CheckersBot.Extensions;
using CheckersBot.Game;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests.Game
{
    public class RankMovesTests
    {
        private IRankMoves _rankMoves;

        [SetUp]
        public void SetUp()
        {
            _rankMoves = new RankMoves();
        }

        [Test]
        public void NotGetKilledIfOtherOptionsExist()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', '.', '.', 'b', '.', 'b', '.', 'b'],
                    ['w', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'w', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var board = boardModel.ConvertToArray();

            var rankToBeKilled = _rankMoves.GetRanks(new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 0, Y = 3},
                        EndingPoint = new Cell {X = 1, Y = 2}
                    }
                }
            }, Team.White, board);

            var rankSaveEdge = _rankMoves.GetRanks(new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 6, Y = 5},
                        EndingPoint = new Cell {X = 7, Y = 4}
                    }
                }
            }, Team.White, board);

            var rankSaveMiddle = _rankMoves.GetRanks(new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 4, Y = 5},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    }
                }
            }, Team.White, board);

            Assert.That(rankToBeKilled.First().Rank < rankSaveEdge.First().Rank);
            Assert.That(rankSaveEdge.First().Rank < rankSaveMiddle.First().Rank);
        }
    }
}
