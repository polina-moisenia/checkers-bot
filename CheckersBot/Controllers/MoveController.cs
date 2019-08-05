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
        public MoveController(ISerializer serializer)
        {
            _serializer = serializer;
            _move = new MoveService();
        }

        [HttpPost]
        public IActionResult Post([FromBody] BoardModel board)
        {
            //var nextMove = _move.GetNextMove(board);
            var response = Content(_serializer.Serialize(new List<Move> {new Move()}), "application/json");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
