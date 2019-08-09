using CheckersBot.Extensions;
using CheckersBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckersBot.Game
{
    public static class PossibleKingsBeats
    {
        internal static List<List<Move>> GetPossibleKingsBeats(CellState[,] boardArray, Team teamPlaying)
        {
            var kings = new List<Cell>();
            var possibleBeats = new List<List<Move>>();


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardArray[i, j] == CellState.WhiteKing && teamPlaying == Team.White ||
                        boardArray[i, j] == CellState.BlackKing && teamPlaying == Team.Black)
                    {
                        kings.Add(new Cell { X = i, Y = j });
                    }
                }
            }

            kings.ForEach((king) =>
            {
                possibleBeats.Add(GetIndividualKingBeats(king, boardArray, teamPlaying, new List<Move>()));
            });

            return possibleBeats.OrderByDescending(x => x.Count()).Take(1).ToList();

            // possibleBeats.Add(new List<Move> { });
            // дерево
            // 1 если по одной из диагоналей есть шашка или король противника
            // 2 и за ним есть пустая клетка
            // 3 бить
            // 4 конечным ходом ничего не выбирать, а запушить в массив все возможные точки остановки
            // 5 для каждой из клеток повторить с пункта 1 до 5

            // выбрать самый длинный ход из доступных для текущего короля и вернуть его
        }

        private static List<Move> GetIndividualKingBeats(Cell king, CellState[,] boardArray, Team teamPlaying, List<Move> beats)
        {
            var clonedBeats = Clone.CloneJson(beats);
            var possibleGoTos = new List<Cell>();
            // top right
            for (var count = 1; king.X + count <= 7 && king.Y - count >= 0; count++)
            {
                if ((boardArray[king.X + count, king.Y - count] == CellState.BlackKing && teamPlaying == Team.White) ||
                    (boardArray[king.X + count, king.Y - count] == CellState.BlackPiece && teamPlaying == Team.White) ||
                    (boardArray[king.X + count, king.Y - count] == CellState.WhiteKing && teamPlaying == Team.Black) ||
                    (boardArray[king.X + count, king.Y - count] == CellState.WhitePiece && teamPlaying == Team.Black))
                {
                    count++;
                    for (; king.X + count <= 7 && king.Y - count >= 0; count++)
                    {
                        if (boardArray[king.X + count, king.Y - count] != CellState.Empty)
                        {
                            break;
                        }
                        if (boardArray[king.X + count, king.Y - count] == CellState.Empty)
                        {
                            possibleGoTos.Add(new Cell
                            {
                                X = king.X + count,
                                Y = king.Y - count,
                            });
                        }
                    }

                    break;
                } else if (boardArray[king.X + count, king.Y - count] != CellState.Empty) { break; }
            }
            // top left
            for (var count = 1; king.X - count >= 0 && king.Y - count >= 0; count++)
            {
                if ((boardArray[king.X - count, king.Y - count] == CellState.BlackKing && teamPlaying == Team.White) ||
                    (boardArray[king.X - count, king.Y - count] == CellState.BlackPiece && teamPlaying == Team.White) ||
                    (boardArray[king.X - count, king.Y - count] == CellState.WhiteKing && teamPlaying == Team.Black) ||
                    (boardArray[king.X - count, king.Y - count] == CellState.WhitePiece && teamPlaying == Team.Black))
                {
                    count++;
                    for (; king.X - count >= 0 && king.Y - count >= 0; count++)
                    {
                        if (boardArray[king.X - count, king.Y - count] != CellState.Empty)
                        {
                            break;
                        }
                        if (boardArray[king.X - count, king.Y - count] == CellState.Empty)
                        {
                            possibleGoTos.Add(new Cell
                            {
                                X = king.X - count,
                                Y = king.Y - count,
                            });
                        }
                    }

                    break;
                } else if (boardArray[king.X - count, king.Y - count] != CellState.Empty) { break; }
            }
            // bottom left
            for (var count = 1; king.X - count >= 0 && king.Y + count <= 7; count++)
            {
                if ((boardArray[king.X - count, king.Y + count] == CellState.BlackKing && teamPlaying == Team.White) ||
                    (boardArray[king.X - count, king.Y + count] == CellState.BlackPiece && teamPlaying == Team.White) ||
                    (boardArray[king.X - count, king.Y + count] == CellState.WhiteKing && teamPlaying == Team.Black) ||
                    (boardArray[king.X - count, king.Y + count] == CellState.WhitePiece && teamPlaying == Team.Black))
                {
                    count++;
                    for (; king.X - count >= 0 && king.Y + count <= 7; count++)
                    {
                        if (boardArray[king.X - count, king.Y + count] != CellState.Empty)
                        {
                            break;
                        }
                        if (boardArray[king.X - count, king.Y + count] == CellState.Empty)
                        {
                            possibleGoTos.Add(new Cell
                            {
                                X = king.X - count,
                                Y = king.Y + count,
                            });
                        }
                    }

                    break;
                } else if (boardArray[king.X - count, king.Y + count] != CellState.Empty) { break; }
            }
            // bottom right
            for (var count = 1; king.X + count <= 7 && king.Y + count <= 7; count++)
            {
                if ((boardArray[king.X + count, king.Y + count] == CellState.BlackKing && teamPlaying == Team.White) ||
                    (boardArray[king.X + count, king.Y + count] == CellState.BlackPiece && teamPlaying == Team.White) ||
                    (boardArray[king.X + count, king.Y + count] == CellState.WhiteKing && teamPlaying == Team.Black) ||
                    (boardArray[king.X + count, king.Y + count] == CellState.WhitePiece && teamPlaying == Team.Black))
                {
                    count++;
                    for (; king.X + count <= 7 && king.Y + count <= 7; count++)
                    {
                        if (boardArray[king.X + count, king.Y + count] != CellState.Empty)
                        {
                            break;
                        }
                        if (boardArray[king.X + count, king.Y + count] == CellState.Empty)
                        {
                            possibleGoTos.Add(new Cell
                            {
                                X = king.X + count,
                                Y = king.Y + count,
                            });
                        }
                    }

                    break;
                } else if (boardArray[king.X + count, king.Y + count] != CellState.Empty) { break; }
            }

            if (possibleGoTos.Count == 0) return new List<Move>();

            List<List<Move>> possibleBeats = new List<List<Move>>();
            possibleGoTos.ForEach((possibleGoTo) =>
            {
                var move = new Move
                {
                    StartingPoint = king,
                    EndingPoint = possibleGoTo,
                };

                var newBoardArray = BoardExtensions.UpdateFromMoves(boardArray, new List<Move>() { move });

                var beatsList = new List<Move>();
                beatsList.Add(move);
                var newBeats = GetIndividualKingBeats(possibleGoTo, newBoardArray, teamPlaying, beatsList);
                if (newBeats.Count > 1) {
                    beatsList = newBeats;
                }
                possibleBeats.Add(beatsList);
            });

            if (possibleBeats.Count > 0)

            clonedBeats.AddRange(possibleBeats.OrderByDescending(x => x.Count()).First());
            return clonedBeats;
        }
    }
}
