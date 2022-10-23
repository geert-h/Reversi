using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        public void CheckPossibleCells()
        {
            for (int yCell = 0; yCell < n; yCell++)
            {
                for (int xCell = 0; xCell < n; xCell++)
                {
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

                    foreach ((int x, int y) in CheckNeigbours(otherPlayer))
                    {
                        int xDistanceCurrentPlayer = 0, yDistanceCurrentPlayer = 0;
                        Console.WriteLine($"x: {x}, y: {y}");

                        for (int i = 1; xCell + x * i >= 0 && xCell + x * i < n && yCell + y * i >= 0 && yCell + y * i < n; i++)
                        {
                            if (table[xCell + x * i, yCell + y * i] == currentPlayer)
                            {
                                xDistanceCurrentPlayer = Math.Abs(x * i);
                                yDistanceCurrentPlayer = Math.Abs(y * i);
                            }
                        };
                        if (xDistanceCurrentPlayer != 0 || yDistanceCurrentPlayer != 0)
                        {
                            for (int i = 1; xCell + x * i >= 0 && xCell + x * i < n && yCell + y * i >= 0 && yCell + y * i < n; i++)
                            {
                                if (table[xCell + x * i, yCell + y * i] == otherPlayer && (xDistanceCurrentPlayer > Math.Abs(x * i) || yDistanceCurrentPlayer > Math.Abs(y * i)))
                                {
                                    table[xCell, yCell] = CellState.Available;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
