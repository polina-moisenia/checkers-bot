using System.Collections.Generic;
using CheckersBot.Models;
using CheckersBot.Serialization;
using CheckersBot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckersBot.Controllers
{
    [Route("move")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly ISerializer _serializer;
        private readonly IMove _move;

        public MoveController(ISerializer serializer, IMove moveService)
        {
            _serializer = serializer;
            _move = moveService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] BoardModel board)
        {
            //var nextMove = _move.GetNextMove(board);
            var nextMove = new List<Move>
            {
                new Move
                {
                    StartingPoint = new Cell {X = 0, Y = 5},
                    EndingPoint = new Cell {X = 1, Y = 4},
                }
            };

            var response = Content(_serializer.Serialize(nextMove), "application/json");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
