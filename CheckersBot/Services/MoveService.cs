using System;
using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Services
{
    public class MoveService : IMove
    {
        public List<Move> GetNextMove(BoardModel currentBoard)
        {
            Random random = new Random();
            var beats = GetPossibleBeats(currentBoard);

            if (beats.Count > 0)
            {
                return beats[0];
            }
            else
            {
                var board = ConvertBoardToArray(currentBoard);
                var possibleMoves = GetPossibleMoves(board, currentBoard.TeamToMoveNext);
                return new List<Move> { possibleMoves[random.Next(0, possibleMoves.Count)] };
            }
        }

        private CellState[,] ConvertBoardToArray(BoardModel currentBoard)
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

        public List<List<Move>> GetPossibleBeats(BoardModel currentBoard)
        {
            var boardArray = ConvertBoardToArray(currentBoard);
            var teamPlaying = currentBoard.TeamToMoveNext;
            var possibleMoves = new List<List<Move>>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardArray[i, j] == CellState.WhitePiece && teamPlaying == Team.White)
                    {
                        if (i - 2 >= 0 && j - 2 >= 0 &&
                            boardArray[i - 1, j - 1] == CellState.BlackPiece && boardArray[i - 2, j - 2] == CellState.Empty)
                        {
                            possibleMoves.Add(new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j - 2 }
                                }
                            });
                        }
                        if (i + 2 <= 7 && j - 2 >= 0 &&
                            boardArray[i + 1, j - 1] == CellState.BlackPiece && boardArray[i + 2, j - 2] == CellState.Empty)
                        {
                            possibleMoves.Add(new List<Move>
                            {
                                new Move {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j - 2 }
                                }
                            });
                        }
                    }
                    else if (boardArray[i, j] == CellState.BlackPiece && teamPlaying == Team.Black)
                    {
                        if (i + 2 <= 7 && j + 2 <= 7 &&
                            boardArray[i + 1, j + 1] == CellState.WhitePiece && boardArray[i + 2, j + 2] == CellState.Empty)
                        {
                            possibleMoves.Add(new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j + 2 }
                                }
                            });
                        }
                        if (i - 2 >= 0 && j + 2 <= 7 &&
                            boardArray[i - 1, j + 1] == CellState.WhitePiece && boardArray[i - 2, j + 2] == CellState.Empty)
                        {
                            possibleMoves.Add(new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j + 2 }
                                }
                            });
                        }
                    }
                }
            }

            return possibleMoves;
            //return new List<List<Move>>();
        }

        public List<Move> GetPossibleMoves(CellState[,] board, Team teamPlaying)
        {
            var possibleMoves = new List<Move>();

            int verticalIncrement = teamPlaying == Team.White ? -1 : 1;
            var incterements = new List<Cell>
            {
                new Cell{ X = 1, Y = verticalIncrement },
                new Cell{ X = -1, Y = verticalIncrement }
            };

            foreach (var incr in incterements)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == CellState.BlackPiece && teamPlaying == Team.Black ||
                            board[i, j] == CellState.WhitePiece && teamPlaying == Team.White)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell { X = i, Y = j },
                                EndingPoint = new Cell { X = i + incr.X, Y = j + incr.Y }
                            };

                            if (ValidMove(nextMove, board))
                            {
                                possibleMoves.Add(nextMove);
                            }
                        }
                    }
                }
            }

            return possibleMoves;
        }

        private bool ValidMove(Move nextMove, CellState[,] board)
        {
            return nextMove.EndingPoint.X < 8 && nextMove.EndingPoint.X >= 0 &&
                   nextMove.EndingPoint.Y < 8 && nextMove.EndingPoint.Y >= 0 &&
                   board[nextMove.EndingPoint.X, nextMove.EndingPoint.Y] == CellState.Empty;
        }
    }
}
