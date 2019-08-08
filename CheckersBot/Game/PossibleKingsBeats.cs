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
            List<List<Move>> possibleBeats = new List<List<Move>>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardArray[i, j] == CellState.WhiteKing && teamPlaying == Team.White)
                    {
                        // if top diagonal with empty add start move, and check the last one
                        Cell opponentCoordinates = null;
                        for (var count = 1; i + count <= 7 && j - count >= 0; count++)
                        {
                            if (boardArray[i + count, j - count] == CellState.BlackKing || boardArray[i + count, j - count] == CellState.BlackPiece)
                            {
                                opponentCoordinates = new Cell { X = i + count, Y = j - count };
                                break;
                            }
                        }

                        if (opponentCoordinates != null)
                        {
                            for (var count = 1; opponentCoordinates.X + count <= 7 && opponentCoordinates.Y - count >= 0; count++)
                            {
                                if (boardArray[opponentCoordinates.X + count, opponentCoordinates.Y - count] == CellState.Empty)
                                {
                                    //opponentCoordinates = new Cell { X = i + count, Y = j - count };

                                    possibleBeats.Add(new List<Move>
                                    {
                                        new Move
                                        {
                                            StartingPoint = new Cell {X = i, Y = j},
                                            EndingPoint = new Cell {X = opponentCoordinates.X + count, Y = opponentCoordinates.Y - count}
                                        }
                                    });
                                    break;
                                }
                            }
                        }
                    }
                    else if (boardArray[i, j] == CellState.BlackKing && teamPlaying == Team.Black)
                    {

                    }
                }
            }

            // possibleBeats.Add(new List<Move> { });
            // дерево
            // 1 если по одной из диагоналей есть шашка или король противника
            // 2 и за ним есть пустая клетка
            // 3 бить
            // 4 конечным ходом ничего не выбирать, а запушить в массив все возможные точки остановки
            // 5 для каждой из клеток повторить с пункта 1 до 5

            // выбрать самый длинный ход из доступных для текущего короля и вернуть его
            return possibleBeats;
        }
    }
}
