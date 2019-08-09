using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests
{
    class UpdateFromKingMovesTests
    {
        private CellState[,] board;

        [Test]
        public void OneBeatMove()
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
            board = boardModel.ConvertToArray();

            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 0, Y = 7},
                    EndingPoint = new Cell {X = 3, Y = 4}
                }
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', 'W', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void TwoBeatMoves()
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
            board = boardModel.ConvertToArray();

            var move = new List<Move>
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
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', 'W', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void TwoBeatMovesWithLength()
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
            board = boardModel.ConvertToArray();

            var move = new List<Move>
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
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', 'W', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }

        [Test]
        public void TwoBeatMovesWithAngleAtLength()
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
            board = boardModel.ConvertToArray();

            var move = new List<Move>
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
            };

            var newBoard = board.UpdateFromMoves(move);

            string expectedNewBoard = @"{
                'team': 'w',
                'field': [
                    ['.', '.', '.', 'W', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.'],
                    ['.', '.', '.', '.', '.', '.', '.', '.']
                ] 
            }";
            var expectedBoard = JsonConvert.DeserializeObject<BoardModel>(expectedNewBoard).ConvertToArray();

            CollectionAssert.AreEqual(expectedBoard, newBoard);
        }
    }
}
