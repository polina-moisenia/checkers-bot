using System.Collections.Generic;
using CheckersBot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CheckersBot.Tests
{
    public abstract class MoveTestBase
    {
        public void CheckListOfListOfMoves(List<List<Move>> moves, List<List<Move>> expectedMoves)
        {
            Assert.AreEqual(moves.Count, expectedMoves.Count);

            var movesStr = JsonConvert.SerializeObject(moves);
            var actualMovesStr = JsonConvert.SerializeObject(expectedMoves);
            Assert.AreEqual(movesStr, actualMovesStr);
        }
    }
}
