using System;
using System.Collections.Generic;
using CheckersBot.Extensions;
using CheckersBot.Models;

namespace CheckersBot.Game
{
    public class PossibleBeatsCalc : IPossibleBeatsCalc
    {
        public List<List<Move>> GetPossibleBeats(CellState[,] boardArray, Team teamPlaying)
        {
            var possibleBeats = new List<List<Move>>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((boardArray[i, j] == CellState.WhitePiece || boardArray[i, j] == CellState.WhiteKing) && teamPlaying == Team.White)
                    {
                        if (i - 2 >= 0 && j - 2 >= 0 &&
                            (boardArray[i - 1, j - 1] == CellState.BlackPiece || boardArray[i - 1, j - 1] == CellState.BlackKing) &&
                            boardArray[i - 2, j - 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j - 2 }
                                }
                            };
                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                        if (i + 2 <= 7 && j - 2 >= 0 &&
                            (boardArray[i + 1, j - 1] == CellState.BlackPiece || boardArray[i + 1, j - 1] == CellState.BlackKing) && 
                            boardArray[i + 2, j - 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j - 2 }
                                }
                            };
                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }

                        if (i - 2 >= 0 && j + 2 <= 7 &&
                            (boardArray[i - 1, j + 1] == CellState.BlackPiece || boardArray[i - 1, j + 1] == CellState.BlackKing) &&
                            boardArray[i - 2, j + 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j + 2 }
                                }
                            };
                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }

                        if (i + 2 <= 7 && j + 2 <= 7 &&
                            (boardArray[i + 1, j + 1] == CellState.BlackPiece || boardArray[i + 1, j + 1] == CellState.BlackKing) &&
                            boardArray[i + 2, j + 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j + 2 }
                                }
                            };
                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                    }
                    else if ((boardArray[i, j] == CellState.BlackPiece || boardArray[i, j] == CellState.BlackKing) && teamPlaying == Team.Black)
                    {
                        if (i + 2 <= 7 && j + 2 <= 7 &&
                            (boardArray[i + 1, j + 1] == CellState.WhitePiece || boardArray[i + 1, j + 1] == CellState.WhiteKing)
                            && boardArray[i + 2, j + 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j + 2 }
                                }
                            };

                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                        if (i - 2 >= 0 && j + 2 <= 7 &&
                            (boardArray[i - 1, j + 1] == CellState.WhitePiece || boardArray[i - 1, j + 1] == CellState.WhiteKing)
                            && boardArray[i - 2, j + 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j + 2 }
                                }
                            };

                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                        if (i + 2 <= 7 && j - 2 >= 0 &&
                            (boardArray[i + 1, j - 1] == CellState.WhitePiece || boardArray[i + 1, j - 1] == CellState.WhiteKing)
                            && boardArray[i + 2, j - 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i + 2, Y = j - 2 }
                                }
                            };

                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                        if (i - 2 >= 0 && j - 2 >= 0 &&
                            (boardArray[i - 1, j - 1] == CellState.WhitePiece || boardArray[i - 1, j - 1] == CellState.WhiteKing)
                            && boardArray[i - 2, j - 2] == CellState.Empty)
                        {
                            var beats = new List<Move>
                            {
                                new Move
                                {
                                    StartingPoint = new Cell { X = i, Y = j },
                                    EndingPoint = new Cell { X = i - 2, Y = j - 2 }
                                }
                            };

                            appendSubsequentBeats(Clone.CloneJson(boardArray), teamPlaying, beats);
                            possibleBeats.Add(beats);
                        }
                    }
                }
            }

            return possibleBeats;
        }

        private void appendSubsequentBeats(CellState[,] boardArray, Team teamPlaying, List<Move> beats)
        {
            var beat = beats[beats.Count - 1];
            var checker = boardArray[beat.StartingPoint.X, beat.StartingPoint.Y];
            boardArray[beat.StartingPoint.X, beat.StartingPoint.Y] = CellState.Empty;
            boardArray[(beat.StartingPoint.X + beat.EndingPoint.X) / 2, (beat.StartingPoint.Y + beat.EndingPoint.Y) / 2] = 
                CellState.Empty;
            boardArray[beat.EndingPoint.Y, beat.EndingPoint.Y] = checker;

            var i = beat.EndingPoint.X;
            var j = beat.EndingPoint.Y;
            if (teamPlaying == Team.White)
            {
                if (i - 2 >= 0 && j - 2 >= 0 &&
                    (boardArray[i - 1, j - 1] == CellState.BlackPiece || boardArray[i - 1, j - 1] == CellState.BlackKing) && boardArray[i - 2, j - 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i - 2, Y = j - 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i + 2 <= 7 && j - 2 >= 0 &&
                  (boardArray[i + 1, j - 1] == CellState.BlackPiece || boardArray[i + 1, j - 1] == CellState.BlackKing) && boardArray[i + 2, j - 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i + 2, Y = j - 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i + 2 <= 7 && j + 2 <= 7 &&
                  (boardArray[i + 1, j + 1] == CellState.BlackPiece || boardArray[i + 1, j + 1] == CellState.BlackKing) && boardArray[i + 2, j + 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i + 2, Y = j + 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i - 2 >= 0 && j + 2 <= 7 &&
                  (boardArray[i - 1, j + 1] == CellState.BlackPiece || boardArray[i - 1, j + 1] == CellState.BlackKing) && boardArray[i - 2, j + 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i - 2, Y = j + 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
            }
            else if (teamPlaying == Team.Black)
            {
                if (i + 2 <= 7 && j + 2 <= 7 &&
                    (boardArray[i + 1, j + 1] == CellState.WhitePiece || boardArray[i + 1, j + 1] == CellState.WhiteKing) && boardArray[i + 2, j + 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i + 2, Y = j + 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i - 2 >= 0 && j + 2 <= 7 &&
                  (boardArray[i - 1, j + 1] == CellState.WhitePiece || boardArray[i - 1, j + 1] == CellState.WhiteKing) && boardArray[i - 2, j + 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i - 2, Y = j + 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i - 2 >= 0 && j - 2 >= 0 &&
                  (boardArray[i - 1, j - 1] == CellState.WhitePiece || boardArray[i - 1, j - 1] == CellState.WhiteKing) && boardArray[i - 2, j - 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i - 2, Y = j - 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
                else if (i + 2 <= 7 && j - 2 >= 0 &&
                  (boardArray[i + 1, j - 1] == CellState.WhitePiece || boardArray[i + 1, j - 1] == CellState.WhiteKing) && boardArray[i + 2, j - 2] == CellState.Empty)
                {
                    beats.Add(new Move
                    {
                        StartingPoint = new Cell { X = i, Y = j },
                        EndingPoint = new Cell { X = i + 2, Y = j - 2 }
                    });
                    appendSubsequentBeats(boardArray, teamPlaying, beats);
                }
            }
        }
    }
}
