using System.Collections.Generic;
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
        public void WhiteMovesTest()
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

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 1, Y = 4},
                        StartingPoint = new Cell {X = 0, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 3, Y = 4},
                        StartingPoint = new Cell {X = 2, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 1, Y = 4},
                        StartingPoint = new Cell {X = 2, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 5, Y = 4},
                        StartingPoint = new Cell {X = 4, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 3, Y = 4},
                        StartingPoint = new Cell {X = 4, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 7, Y = 4},
                        StartingPoint = new Cell {X = 6, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        EndingPoint = new Cell {X = 5, Y = 4},
                        StartingPoint = new Cell {X = 6, Y = 5}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void BlackMovesTest()
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

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 2},
                        EndingPoint = new Cell {X = 2, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 2},
                        EndingPoint = new Cell {X = 0, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 1},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 4, Y = 1},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 4, Y = 3},
                        EndingPoint = new Cell {X = 5, Y = 4}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 4, Y = 3},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 6, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 7, Y = 2},
                        EndingPoint = new Cell {X = 6, Y = 3}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void BlackKingMovesTest()
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

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 6, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 7, Y = 4}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 4, Y = 1}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 4, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void WhiteKingMovesTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', 'b', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', '.', '.', 'B', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'W', '.', '.', '.', '.', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _movesCalc.GetPossibleMoves(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 1, Y = 6}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 0, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 6}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 4, Y = 5}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 5, Y = 4}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 6, Y = 3}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 7},
                        EndingPoint = new Cell {X = 7, Y = 2}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }
    }
}
