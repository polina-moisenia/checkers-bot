using CheckersBot.Models;

namespace CheckersBot.Extensions
{
    public static class BoardExtensions
    {
        public static CellState[,] ConvertToArray(this BoardModel currentBoard)
        {
            var board = new CellState[8, 8];

            int row_index = 0;
            foreach (var row in currentBoard.Rows)
            {
                int column_index = 0;
                foreach (var cell in row)
                {
                    board[column_index, row_index] = cell;
                    column_index++;
                }
                row_index++;
            }

            return board;
        }
    }
}
