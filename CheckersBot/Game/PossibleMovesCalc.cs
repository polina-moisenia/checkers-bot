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
            var incterements = new List<Cell>
            {
                new Cell {X = 1, Y = verticalIncrement},
                new Cell {X = -1, Y = verticalIncrement}
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
                                StartingPoint = new Cell {X = i, Y = j},
                                EndingPoint = new Cell {X = i + incr.X, Y = j + incr.Y}
                            };

                            if (ValidMove(nextMove, board))
                            {
                                possibleMoves.Add(nextMove);
                            }
                        }

                        //TODO add kings
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
