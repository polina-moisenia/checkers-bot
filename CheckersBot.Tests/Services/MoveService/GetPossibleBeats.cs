using System;
using Xunit;
using CheckersBot.Services;
using CheckersBot.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckersBot.Tests
{
    public class GetPossibleBeatsTests
    {
        private readonly MoveService _moveService;

        public GetPossibleBeatsTests()
        {
            _moveService = new MoveService();
        }

        [Fact]
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
            var move = _moveService.GetPossibleBeats(boardModel);
            var actualMoves = new List<List<Move>>
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

            var moveStr = JsonConvert.SerializeObject(move);
            var actualMovesStr = JsonConvert.SerializeObject(actualMoves);
            Assert.Equal(moveStr, actualMovesStr);
        }

        [Fact]
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
            var move = _moveService.GetPossibleBeats(boardModel);
            var actualMoves = new List<List<Move>>
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

            var moveStr = JsonConvert.SerializeObject(move);
            var actualMovesStr = JsonConvert.SerializeObject(actualMoves);
            Assert.Equal(moveStr, actualMovesStr);
        }

        [Fact]
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
            var move = _moveService.GetPossibleBeats(boardModel);
            var actualMoves = new List<List<Move>>
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

            var moveStr = JsonConvert.SerializeObject(move);
            var actualMovesStr = JsonConvert.SerializeObject(actualMoves);
            Assert.Equal(moveStr, actualMovesStr);
        }


        [Fact]
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
            var move = _moveService.GetPossibleBeats(boardModel);
            var actualMoves = new List<List<Move>>
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

            var moveStr = JsonConvert.SerializeObject(move);
            var actualMovesStr = JsonConvert.SerializeObject(actualMoves);
            Assert.Equal(moveStr, actualMovesStr);
        }

        [Fact]
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
            var move = _moveService.GetPossibleBeats(boardModel);
            var actualMoves = new List<List<Move>>
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

            var moveStr = JsonConvert.SerializeObject(move);
            var actualMovesStr = JsonConvert.SerializeObject(actualMoves);
            Assert.Equal(moveStr, actualMovesStr);
        }
    }
}
