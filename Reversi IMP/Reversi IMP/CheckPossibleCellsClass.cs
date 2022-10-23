using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        public void CheckPossibleCells()
        {

            CellState currentPlayer = CellState.None;
            CellState otherPlayer = CellState.None;

            switch (move % 2)
            {
                case 0:
                    currentPlayer = CellState.Player1;
                    otherPlayer = CellState.Player2;
                    break;

                case 1:
                    currentPlayer = CellState.Player2;
                    otherPlayer = CellState.Player1;
                    break;
            }

            for (int yCell = 0; yCell < n; yCell++)
            {
                for (int xCell = 0; xCell < n; xCell++)
                {
                    if (table[xCell, yCell] != CellState.None) continue;

                    foreach ((int x, int y) in CheckNeigbours(otherPlayer, xCell, yCell))
                    {
                        int xDistanceCurrentPlayer = 0, yDistanceCurrentPlayer = 0;
                        int xFirstEmptyCell = 0; int yFirstEmptyCell = 0;

                        for (int i = 1; xCell + x * i >= 0 && xCell + x * i < n && yCell + y * i >= 0 && yCell + y * i < n; i++)
                        {
                            if (table[xCell + x * i, yCell + y * i] == currentPlayer)
                            {
                                xDistanceCurrentPlayer = Math.Abs(x * i);
                                yDistanceCurrentPlayer = Math.Abs(y * i);
                            }
                            if ((table[xCell + x * i, yCell + y * i] == CellState.None || table[xCell + x * i, yCell + y * i] == CellState.Available) && xFirstEmptyCell == 0 && yFirstEmptyCell == 0)
                            {
                                xFirstEmptyCell = Math.Abs(x * i);
                                yFirstEmptyCell = Math.Abs(y * i);
                            }
                        }

                        if (xDistanceCurrentPlayer == 0 && yDistanceCurrentPlayer == 0) continue;

                        for (int i = 1; xCell + x * i >= 0 && xCell + x * i < n && yCell + y * i >= 0 && yCell + y * i < n; i++)
                        {
                            if (table[xCell + x * i, yCell + y * i] == otherPlayer && (xFirstEmptyCell > xDistanceCurrentPlayer || yFirstEmptyCell > yDistanceCurrentPlayer) && (xDistanceCurrentPlayer > Math.Abs(x * i) || yDistanceCurrentPlayer > Math.Abs(y * i)))
                            {
                                table[xCell, yCell] = CellState.Available;
                                Console.WriteLine($"Available: ({xCell}, {yCell})");
                            }
                        }
                    }
                }
            }
        }
    }
}
