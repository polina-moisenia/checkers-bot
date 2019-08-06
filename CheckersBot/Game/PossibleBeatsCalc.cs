using System.Collections.Generic;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class PossibleBeatsCalc : IPossibleBeatsCalc
    {
        public List<List<Move>> GetPossibleBeats(CellState[,] boardArray, Team teamPlaying)
        { 
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
        }
    }
}
