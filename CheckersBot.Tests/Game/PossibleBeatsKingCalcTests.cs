using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Game;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests.Game
{
    public class PossibleBeatsKingCalcTests : MoveTestBase
    {
        private IPossibleBeatsCalc _beatsCalc;

        [SetUp]
        public void SetUp()
        {
            _beatsCalc = new PossibleBeatsCalc();
        }

        [Test]
        public void OneSimpleBeatTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', 'b', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 2, Y = 5}
                    }
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }


        [Test]
        public void OnePossibleBeatTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 4}
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
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', 'b', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void TwoPossibleBeatsWithLengthTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', 'b', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 6, Y = 1}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void ThreePossibleBeatsWithLengthTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', 'b', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 7, Y = 0}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void TwoPossibleBeatsInAngleTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 3, Y = 4}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 1, Y = 2}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void TwoPossibleBeatsInAngleAtLengthTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', 'b', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 3, Y = 0}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }

        [Test]
        public void ThreePossibleBeatsInAngleAtLengthTest()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', 'b', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['W', '.', '.', '.', '.', '.', '.', '.']
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
                        StartingPoint = new Cell {X = 0, Y = 7},
                        EndingPoint = new Cell {X = 5, Y = 2}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 5, Y = 2},
                        EndingPoint = new Cell {X = 3, Y = 0}
                    },
                    new Move
                    {
                        StartingPoint = new Cell {X = 3, Y = 0},
                        EndingPoint = new Cell {X = 7, Y = 4}
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }


        [Test]
        public void NoBeatsTwoInARow()
        {
            string jsonData = @"{
                'team': 'b',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', 'w', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', 'w', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'B', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            var moves = _beatsCalc.GetPossibleBeats(boardModel.ConvertToArray(), boardModel.TeamToMoveNext);

            var expectedMoves = new List<List<Move>>
            {
                
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }



        [Test]
        public void NoBeatsTwoInARowAgain()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', 'W', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'w', '.', '.', '.', '.', '.'],
                    ['.', 'b', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
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
                    },
                }
            };

            CheckListOfListOfMoves(moves, expectedMoves);
        }
    }
}