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
        public void Test1()
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
    }
}
