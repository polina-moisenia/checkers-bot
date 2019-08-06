using System.Collections.Generic;
using System.Linq;
using CheckersBot.Extensions;
using CheckersBot.Game;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests.Game
{
    public class PossibleMovesCalcTests : MoveTestBase
    {
        private IPossibleMovesCalc _movesCalc;

        [SetUp]
        public void SetUp()
        {
            _movesCalc = new PossibleMovesCalc();
        }

        [Test]
        public void GameStartTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _movesCalc.GetPossibleMoves(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<Move>
            {
                new Move
                {
                    EndingPoint = new Cell {X = 1, Y = 4},
                    StartingPoint = new Cell {X = 0, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 1, Y = 4},
                    StartingPoint = new Cell {X = 2, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 3, Y = 4},
                    StartingPoint = new Cell {X = 2, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 3, Y = 4},
                    StartingPoint = new Cell {X = 4, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 4},
                    StartingPoint = new Cell {X = 4, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 4},
                    StartingPoint = new Cell {X = 6, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 7, Y = 4},
                    StartingPoint = new Cell {X = 6, Y = 5}
                }
            };

            CheckListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void GameBlackTest()
        {
            string jsonData = @"{
                'team': 'b',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', 'b', '.', 'b'],
                    ['.', '.', '.', '.', 'b', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _movesCalc.GetPossibleMoves(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<Move>
            {
                new Move
                {
                    EndingPoint = new Cell {X = 0, Y = 4},
                    StartingPoint = new Cell {X = 1, Y = 3}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 3, Y = 4},
                    StartingPoint = new Cell {X = 1, Y = 3}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 3, Y = 2},
                    StartingPoint = new Cell {X = 2, Y = 1}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 3, Y = 2},
                    StartingPoint = new Cell {X = 4, Y = 1}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 4, Y = 3},
                    StartingPoint = new Cell {X = 3, Y = 4}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 4, Y = 3},
                    StartingPoint = new Cell {X = 5, Y = 4}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 2},
                    StartingPoint = new Cell {X = 6, Y = 3}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 2},
                    StartingPoint = new Cell {X = 6, Y = 2}
                }
            };

            CheckListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void GameKingBlackTest()
        {
            string jsonData = @"{
                'team': 'b',
                'field': [
                    ['.', '.', '.', 'W', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'w', '.'],
                    ['.', '.', '.', '.', '.', 'B', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'w', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _movesCalc.GetPossibleMoves(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<Move>
            {
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 3},
                    StartingPoint = new Cell {X = 4, Y = 2}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 3},
                    StartingPoint = new Cell {X = 4, Y = 4}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 3},
                    StartingPoint = new Cell {X = 3, Y = 5}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 3},
                    StartingPoint = new Cell {X = 6, Y = 4}
                },
                new Move
                {
                    EndingPoint = new Cell {X = 5, Y = 3},
                    StartingPoint = new Cell {X = 7, Y = 5}
                }
            };

            CheckListOfMoves(moves, expectedMoves);
        }
    }
}
