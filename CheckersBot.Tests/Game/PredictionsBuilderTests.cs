using System.Collections.Generic;
using System.Threading;
using CheckersBot.Extensions;
using CheckersBot.Game;
using CheckersBot.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests.Game
{
    public class PredictionsBuilderTests
    {
        private IPredictionBuilder _predictionBuilder;
        private Mock<IPossibleBeatsCalc> _beatCalc;
        private Mock<IPossibleMovesCalc> _moveCalc;

        private List<List<Move>> _beats;
        private List<List<Move>> _moves;

        private CellState[,] board;

        [SetUp]
        public void SetUp()
        {
            _moves = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell{ X = 0, Y = 1},
                        EndingPoint = new Cell{ X = 1, Y = 2}
                    }
                }
            };

            _beats = new List<List<Move>>
            {
                new List<Move>
                {
                    new Move
                    {
                        StartingPoint = new Cell{ X = 0, Y = 1},
                        EndingPoint = new Cell{ X = 2, Y = 3}
                    }
                }
            };

            _beatCalc = new Mock<IPossibleBeatsCalc>();
            _beatCalc.Setup(beat => beat.GetPossibleBeats(It.IsAny<CellState[,]>(), It.IsAny<Team>())).Returns(_beats);

            _moveCalc = new Mock<IPossibleMovesCalc>();
            _moveCalc.Setup(move => move.GetPossibleMoves(It.IsAny<CellState[,]>(), It.IsAny<Team>())).Returns(_moves);

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

            _predictionBuilder = new PredictionBuilder(_beatCalc.Object, _moveCalc.Object);
        }

        [Test]
        public void OnlyItemsWithSetDepthAreStored()
        {
            var move = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 7, Y = 6},
                    EndingPoint = new Cell {X = 6, Y = 5}
                }
            };

            var node = new PredictionNode(null, move, Team.White, board, 0);
            var prediction = _predictionBuilder.GetDepthwisePrediction(node, 2, It.IsAny<CancellationToken>());

            Assert.That(prediction.Count, Is.EqualTo(4));
            Assert.That(prediction.FindAll(pr => pr.CurrentDepth == 2).Count, Is.EqualTo(4));
        }

    }
}
