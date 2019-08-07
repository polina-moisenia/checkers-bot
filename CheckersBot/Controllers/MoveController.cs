using System.Collections.Generic;
using CheckersBot.Game;
using CheckersBot.Models;
using CheckersBot.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckersBot.Controllers
{
    [Route("move")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly ISerializer _serializer;
        private readonly INextMovesChooser _nextMovesChooser;

        public MoveController(ISerializer serializer, INextMovesChooser nextMovesChooserService)
        {
            _serializer = serializer;
            _nextMovesChooser = nextMovesChooserService;
        }

        [EnableCors("AllowAllPolicy")]
        [HttpPost]
        public IActionResult Post([FromBody] BoardModel board)
        {
            var nextMoves = _nextMovesChooser.GetNextMove(board);

            var response = Content(_serializer.Serialize(nextMoves), "application/json");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
