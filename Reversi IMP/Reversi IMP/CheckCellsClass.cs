using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        (CellState a, CellState b) CurrentPlayer()
        {
            CellState a = CellState.None;
            CellState b = CellState.None;
            switch (move % 2)
            {
                case 0: a = CellState.Player1;
                        b = CellState.Player2;
                    break;
                case 1: a = CellState.Player2;
                        b = CellState.Player1;
                  break;
            }
            return (a, b);
        }

        void CheckCells()
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            if (table[Row, Column] == CellState.Player1 ||
                table[Row, Column] == CellState.Player2) return;

            foreach ((int x, int y) in CheckNeigbours(otherPlayer, Row, Column))
            {
                int xDistanceCurrentPlayer = 0, yDistanceCurrentPlayer = 0;
                int xFirstEmptyCell = 0; int yFirstEmptyCell = 0;

                for (int i = 1; Row + x * i >= 0 &&
                    Row + x * i < n              &&
                    Column + y * i >= 0          &&
                    Column + y * i < n; i++)
                {
                    if (table[Row + x * i, Column + y * i] == currentPlayer)
                    {
                        xDistanceCurrentPlayer = Math.Abs(x * i);
                        yDistanceCurrentPlayer = Math.Abs(y * i);
                    }
                    if ((table[Row + x * i, Column + y * i] == CellState.None      ||
                        table[Row + x * i, Column + y * i] == CellState.Available) &&
                        xFirstEmptyCell == 0                                       &&
                        yFirstEmptyCell == 0)
                    {
                        xFirstEmptyCell = Math.Abs(x * i);
                        yFirstEmptyCell = Math.Abs(y * i);
                    }
                }
                if (xDistanceCurrentPlayer == 0 && yDistanceCurrentPlayer == 0) continue;

                for (int i = 1; Row + x * i >= 0 &&
                    Row + x * i < n              &&
                    Column + y * i >= 0          &&
                    Column + y * i < n; i++)
                {
                    bool firstCurrentPlayerCell = false;

                    if (table[Row + x * i, Column + y * i] == currentPlayer &&
                        (xDistanceCurrentPlayer > Math.Abs(x * i)           ||
                        yDistanceCurrentPlayer > Math.Abs(y * i)))
                        firstCurrentPlayerCell = true;

                    if (table[Row + x * i, Column + y * i] == otherPlayer &&
                        (xFirstEmptyCell > xDistanceCurrentPlayer         ||
                        yFirstEmptyCell > yDistanceCurrentPlayer)         &&
                        (xDistanceCurrentPlayer > Math.Abs(x * i)         ||
                        yDistanceCurrentPlayer > Math.Abs(y * i)))
                    {
                        table[Row, Column] = currentPlayer;
                        if (firstCurrentPlayerCell == true)
                            break;
                        table[Row + x * i, Column + y * i] = currentPlayer;
                    }
                }
            }
            if (table[Row, Column] == currentPlayer)
            {
                move++;
            }

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (table[x, y] == CellState.Available)
                        table[x, y] = CellState.None;
                }
            }
            CheckPossibleCells();
            CheckGameState();
            nthBestMove();
        }

        (int x, int y)[] CheckNeigbours(CellState otherPlayer, int xPos, int yPos)
        {
           List<(int x, int y)>ValidNeighbouringCells = new List<(int x, int y)>();

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (xPos + x < 0  ||
                        yPos + y < 0  ||
                        xPos + x >= n ||
                        yPos + y >= n)
                        continue;
                    if (table[xPos + x, yPos + y] == otherPlayer)
                        ValidNeighbouringCells.Add((x, y));
                }
            }
            return ValidNeighbouringCells.ToArray();
        }
    }
}
