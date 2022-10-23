using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        void CheckCells()
        {

            if (table[Row, Column] != CellState.None) return;

            CellState currentPlayer = CellState.None;
            CellState otherPlayer = CellState.None;
            (int x, int y, CellState state)[] DirectionArray = null;

            switch (move % 2)
            {
                case 0:
                    currentPlayer = CellState.Player1;
                    otherPlayer = CellState.Player2;
                    DirectionArray = GetCellStatesInDirection(CellState.Player1, CellState.Player2);
                    break;

                case 1:
                    currentPlayer = CellState.Player2;
                    otherPlayer = CellState.Player1;
                    DirectionArray = GetCellStatesInDirection(CellState.Player2, CellState.Player1);
                    break;
            }

            if (DirectionArray == null) return;

            foreach ((int x, int y) in CheckNeigbours(otherPlayer))
            {
                int xDistanceCurrentPlayer = 0, yDistanceCurrentPlayer = 0;
                Console.WriteLine($"x: {x}, y: {y}");

                for (int i = 1; Row + x * i >= 0 && Row + x * i < n && Column + y * i >= 0 && Column + y * i < n; i++)
                {
                    if (table[Row + x * i, Column + y * i] == currentPlayer)
                    {
                        xDistanceCurrentPlayer = Math.Abs(x * i);
                        yDistanceCurrentPlayer = Math.Abs(y * i);
                    }
                };
                if (xDistanceCurrentPlayer != 0 || yDistanceCurrentPlayer != 0)
                {
                    for (int i = 1; Row + x * i >= 0 && Row + x * i < n && Column + y * i >= 0 && Column + y * i < n; i++)
                    {
                        bool firstCurrentPlayerCell = false;

                        if (table[Row + x * i, Column + y * i] == currentPlayer && (xDistanceCurrentPlayer > Math.Abs(x * i) || yDistanceCurrentPlayer > Math.Abs(y * i)))
                            firstCurrentPlayerCell = true;

                        if (table[Row + x * i, Column + y * i] == otherPlayer && (xDistanceCurrentPlayer > Math.Abs(x * i) || yDistanceCurrentPlayer > Math.Abs(y * i)))
                        {
                            table[Row, Column] = currentPlayer;
                            if (firstCurrentPlayerCell == true)
                                break;
                            table[Row + x * i, Column + y * i] = currentPlayer;
                        }
                    }
                }
            }
            if (table[Row, Column] == currentPlayer)
            {
                move++;
            }
            CheckPossibleCells();
        }

        (int x, int y)[] CheckNeigbours(CellState otherPlayer)
        {
            List<(int x, int y)> ValidNeighbouringCells = new List<(int x, int y)>();

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (Row + x < 0 || Column + y < 0 || Row + x >= n || Column + y >= n)
                        continue;
                    if (table[Row + x, Column + y] == otherPlayer)
                    {
                        Console.WriteLine($"x: {x}, y:{y}");
                        ValidNeighbouringCells.Add((x, y));
                    }
                }
            }
            return ValidNeighbouringCells.ToArray();
        }

        (int x, int y, CellState state)[] GetCellStatesInDirection(CellState currentPlayer, CellState otherPlayer)
        {
            List<(int x, int y, CellState state)> CellStatesInDirection = new List<(int x, int y, CellState state)>();

            foreach ((int x, int y) in CheckNeigbours(otherPlayer))
            {
                if (Math.Abs(x) == Math.Abs(y))
                {
                    for (int Offset = 0; Row + x * Offset < n && Row + x * Offset >= 0 && Column + y * Offset < n && Column + y * Offset >= 0; Offset++)
                    {
                        if (table[Row + x * Offset, Column + y * Offset] == CellState.None)
                            CellStatesInDirection.Add((x * Offset, y * Offset, CellState.None));
                        else if (table[Row + x * Offset, Column + y * Offset] == currentPlayer)
                            CellStatesInDirection.Add((x * Offset, y * Offset, currentPlayer));
                        else if (table[Row + x * Offset, Column + y * Offset] == otherPlayer)
                            CellStatesInDirection.Add((x * Offset, y * Offset, otherPlayer));
                    }
                }
                else
                {
                    for (int yOffset = 0; Column + y * yOffset >= 0 && Column + y * yOffset < n; yOffset++)
                    {
                        for (int xOffset = 0; Row + x * xOffset >= 0 && Row + x * xOffset < n; xOffset++)
                        {
                            if (table[Row + x * xOffset, Column + y * yOffset] == CellState.None)
                                CellStatesInDirection.Add((x * xOffset, y * yOffset, CellState.None));
                            else if (table[Row + x * xOffset, Column + y * yOffset] == currentPlayer)
                                CellStatesInDirection.Add((x * xOffset, y * yOffset, currentPlayer));
                            else if (table[Row + x * xOffset, Column + y * yOffset] == otherPlayer)
                                CellStatesInDirection.Add((x * xOffset, y * yOffset, otherPlayer));

                            if (x == 0)
                                break;
                        }
                        if (y == 0)
                            break;
                    }
                }
                return CellStatesInDirection.ToArray();
            }
            return null;
        }
    }
}
