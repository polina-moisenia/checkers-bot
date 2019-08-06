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
        public void EmptyTest()
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
            var move = _moveService.GetPossibleBeats(boardModel);
            Assert.Equal(move, new List<List<Move>>());
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
                        StartingPoint = new Cell {X = 1, Y = 5},
                        EndingPoint = new Cell {X = 3, Y = 3}
                    }
                }
            };
            Assert.Equal(move, actualMoves);
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
            Assert.Equal(move, actualMoves);
        }

        [Fact]
        public void TwoPlusTwoPossibleBeatsTest()
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
                        StartingPoint = new Cell {X = 3, Y = 4},
                        EndingPoint = new Cell {X = 5, Y = 4}
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
            Assert.Equal(move, actualMoves);
        }
    }
}
