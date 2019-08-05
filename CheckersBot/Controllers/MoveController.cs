using System.Collections.Generic;
using CheckersBot.Models;
using CheckersBot.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckersBot.Controllers
{
    [Route("move")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly ISerializer _serializer;
        public MoveController(ISerializer serializer)
        {
            _serializer = serializer;
        }

        [HttpPost]
        public IActionResult Post([FromBody] BoardModel value)
        {
            var response = Content(_serializer.Serialize(new List<Move> {new Move()}), "application/json");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
