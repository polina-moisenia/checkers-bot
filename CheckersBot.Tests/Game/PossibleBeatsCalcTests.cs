using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Game;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests.Game
{
    public class PossibleBeatsCalcTests : MoveTestBase
    {
        private IPossibleBeatsCalc _beatsCalc;

        [SetUp]
        public void SetUp()
        {
            _beatsCalc = new PossibleBeatsCalc();
        }

        [Test]
        public void OnePossibleBeatTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', 'b', '.', 'b'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', 'w', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'w', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 4},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void OnePossibleBeatTestBlack()
        {
            string jsonData = @"{
                'team': 'b',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', 'b', '.', 'b'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', 'w', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'w', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 2, Y = 3},
                        EndingPoint = new Cell {X = 0, Y = 5}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void TwoPossibleBeatsTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', 'b', '.'],
                    ['.', 'w', '.', '.', '.', '.', '.', 'w'],
                    ['.', '.', 'w', '.', 'w', '.', '.', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 4},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 7, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void TwoPlusTwoPossibleBeatsTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', '.', '.', 'b'],
                    ['.', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'w', '.', '.', '.', '.', '.', 'w'],
                    ['.', '.', 'w', '.', 'w', '.', '.', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 4},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 2},
                        EndingPoint = new Cell {X = 5, Y = 4}
                    }
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 7, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
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
        public void TwoPlusThreePossibleBeatsTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'w', '.', '.', '.', '.', '.', 'w'],
                    ['.', '.', 'w', '.', 'w', '.', '.', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ]
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 1, Y = 4},
                        EndingPoint = new Cell {X = 3, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 2},
                        EndingPoint = new Cell {X = 5, Y = 4}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 7, Y = 2}
                    },
                },
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell {X = 7, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }
    }
}
