using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests
{
    public class BoardUpdaterTests
    {
        private CellState[,] board;

        [Test]
        public void SingleMove()
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
            board = boardModel.ConvertToArray();

            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 2, Y = 5},
                    EndingPoint = new Cell {X = 3, Y = 4}
                }
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', 'w', '.', '.', '.', '.'],
                    ['w', '.', '.', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void SeveralBeatsMoves()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', 'b', '.', 'b', '.', '.'],
                    ['w', '.', 'w', '.', 'w', '.', '.', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            board = boardModel.ConvertToArray();

            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 2, Y = 5},
                    EndingPoint = new Cell {X = 4, Y = 3}
                },
                new Move
                {
                    StartingPoint = new Cell {X = 4, Y = 3},
                    EndingPoint = new Cell {X = 6, Y = 5}
                }
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['b', '.', 'b', '.', 'b', '.', 'b', '.'],
                    ['.', 'b', '.', 'b', '.', 'b', '.', 'b'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['w', '.', '.', '.', 'w', '.', 'w', '.'],
                    ['.', 'w', '.', 'w', '.', 'w', '.', 'w'],
                    ['w', '.', 'w', '.', 'w', '.', 'w', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void BeatKingAndBeat()
        {
            string jsonData = @"{
                'team': 'b',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', 'W', '.', 'w', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            board = boardModel.ConvertToArray();

            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 6, Y = 5},
                    EndingPoint = new Cell {X = 4, Y = 7}
                },
                new Move
                {
                    StartingPoint = new Cell {X = 4, Y = 7},
                    EndingPoint = new Cell {X = 2, Y = 5}
                }
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'b',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'B', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void SeveralBeatsWithKing()
        {
            string jsonData = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'B', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', 'b', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', '.', '.', 'w', '.', '.'],
                    ['.', '.', '.', '.', 'W', '.', '.', '.']
                ] 
            }";
            var boardModel = JsonConvert.DeserializeObject<BoardModel>(jsonData);
            board = boardModel.ConvertToArray();

            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 4, Y = 7},
                    EndingPoint = new Cell {X = 1, Y = 4}
                },
                new Move
                {
                    StartingPoint = new Cell {X = 1, Y = 4},
                    EndingPoint = new Cell {X = 3, Y = 2}
                }
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'b',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', 'W', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'b', '.'],
                    ['.', '.', '.', '.', '.', 'w', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }
    }
}
