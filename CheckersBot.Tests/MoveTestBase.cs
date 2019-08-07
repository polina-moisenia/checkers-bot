using System.Collections.Generic;
using System.Linq;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests
{
    public abstract class MoveTestBase
    {
        public void CheckListOfMoves(List<Move> moves, List<Move> expectedMoves)
        {
            Assert.AreEqual(moves.Count, expectedMoves.Count);
            foreach (var move in moves)
            {
                Assert.IsNotNull(expectedMoves.Any(m => m.EndingPoint == move.EndingPoint && m.StartingPoint == move.StartingPoint));
            }
        }

        public void CheckListOfListOfMoves(List<List<Move>> moves, List<List<Move>> expectedMoves)
        {
            Assert.AreEqual(moves.Count, expectedMoves.Count);

            var movesStr = JsonConvert.SerializeObject(moves);
            var actualMovesStr = JsonConvert.SerializeObject(expectedMoves);
            Assert.AreEqual(movesStr, actualMovesStr);
        }
    }
}
