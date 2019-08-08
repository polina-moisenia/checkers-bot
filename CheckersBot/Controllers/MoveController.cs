using System.Threading.Tasks;
using CheckersBot.Game;
using CheckersBot.Helpers;
using CheckersBot.Models;
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
        private readonly IGetNextMoves _getNextMoves;

        public MoveController(ISerializer serializer, IGetNextMoves getNextMoves)
        {
            _serializer = serializer;
            _getNextMoves = getNextMoves;
        }

        [EnableCors("AllowAllPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoardModel board)
        {
            var nextMoves = await _getNextMoves.GetMovesBeforeTimeoutAsync(board);

            var response = Content(_serializer.Serialize(nextMoves), "application/json");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
