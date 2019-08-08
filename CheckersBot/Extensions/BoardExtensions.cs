using System;
using System.Collections.Generic;
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

        public static CellState[,] UpdateFromMoves(this CellState[,] currentBoard, List<Move> moves)
        {
            var updatedBoard = currentBoard.CloneJson();
            foreach (var move in moves)
            {
                var check = updatedBoard[move.StartingPoint.X, move.StartingPoint.Y];
                updatedBoard[move.StartingPoint.X, move.StartingPoint.Y] = CellState.Empty;
                if (Math.Abs(move.EndingPoint.X - move.StartingPoint.X) > 1)
                {
                    var xDiff = move.EndingPoint.X - move.StartingPoint.X > 0 ? -1 : 1;
                    var yDiff = move.EndingPoint.Y - move.StartingPoint.Y > 0 ? -1 : 1;

                    updatedBoard[move.EndingPoint.X + xDiff, move.EndingPoint.Y + yDiff] = CellState.Empty;
                }

                if (move.EndingPoint.Y == 0 && check == CellState.WhitePiece)
                {
                    check = CellState.WhiteKing;
                }
                if (move.EndingPoint.Y == 7 && check == CellState.BlackPiece)
                {
                    check = CellState.BlackKing;
                }

                updatedBoard[move.EndingPoint.X, move.EndingPoint.Y] = check;
            }

            return updatedBoard;
        }

        public static GameStats GetBoardStats(this CellState[,] currentBoard)
        {
            var stats = new GameStats();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (currentBoard[i, j] == CellState.BlackKing)
                        stats.BlackKings++;
                    if (currentBoard[i, j] == CellState.BlackPiece)
                        stats.BlackPieces++;
                    if (currentBoard[i, j] == CellState.WhiteKing)
                        stats.WhiteKings++;
                    if (currentBoard[i, j] == CellState.WhitePiece)
                        stats.WhitePieces++;
                }
            }

            return stats;
        }
    }
}
