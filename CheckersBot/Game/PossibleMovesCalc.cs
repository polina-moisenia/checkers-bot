using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class PossibleMovesCalc : IPossibleMovesCalc
    {
        public List<Move> GetPossibleMoves(CellState[,] board, Team teamPlaying)
        {
            var possibleMoves = new List<Move>();

            int verticalIncrement = teamPlaying == Team.White ? -1 : 1;
            var increments = new List<Cell>
            {
                new Cell {X = 1, Y = verticalIncrement},
                new Cell {X = -1, Y = verticalIncrement}
            };


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    foreach (var incr in increments)
                    {
                        if (board[i, j] == CellState.BlackPiece && teamPlaying == Team.Black ||
                            board[i, j] == CellState.WhitePiece && teamPlaying == Team.White)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i + incr.X, Y = j + incr.Y}
                            };

                            if (ValidMove(nextMove, board))
                            {
                                possibleMoves.Add(nextMove);
                            }
                        }

                    }

                    if (board[i, j] == CellState.BlackKing && teamPlaying == Team.Black ||
                        board[i, j] == CellState.WhiteKing && teamPlaying == Team.White)
                    {
                        int diff = 1;
                        while (i + diff < 8 && j + diff < 8 && board[i + diff, j + diff] == CellState.Empty)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i + diff, Y = j + diff}
                            };
                            possibleMoves.Add(nextMove);
                            diff++;
                        }

                        diff = 1;
                        while (i - diff >= 0 && j - diff >= 0 && board[i - diff, j - diff] == CellState.Empty)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i - diff, Y = j - diff}
                            };
                            possibleMoves.Add(nextMove);
                            diff++;
                        }

                        diff = 1;
                        while (i + diff < 8 && j - diff >= 0 && board[i + diff, j - diff] == CellState.Empty)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i + diff, Y = j - diff}
                            };
                            possibleMoves.Add(nextMove);
                            diff++;
                        }

                        diff = 1;
                        while (i - diff >= 0 && j + diff < 8 && board[i - diff, j + diff] == CellState.Empty)
                        {
                            var nextMove = new Move
                            {
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i - diff, Y = j + diff}
                            };
                            possibleMoves.Add(nextMove);
                            diff++;
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
