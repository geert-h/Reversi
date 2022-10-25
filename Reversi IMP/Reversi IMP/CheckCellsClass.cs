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

        void CheckCells(int xCell, int yCell, CellState[,] board)
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            if (board[xCell, yCell] == CellState.Player1 ||
                board[xCell, yCell] == CellState.Player2) return;

            foreach ((int x, int y) in CheckNeigbours(otherPlayer, xCell, yCell, board))
            {
                (int xDistanceCurrentPlayer, int yDistanceCurrentPlayer, int xFirstEmptyCell, int yFirstEmptyCell) = DistanceCheck(xCell, yCell, x, y, currentPlayer, board);

                if (xDistanceCurrentPlayer == 0 && yDistanceCurrentPlayer == 0) continue;

                ChangeBoardStatus(xCell, yCell, x, y, xDistanceCurrentPlayer, yDistanceCurrentPlayer, xFirstEmptyCell, yFirstEmptyCell, board);
            }

            ResetAvailableCells(xCell, yCell, board);
        }

        (int xDistanceCurrentPlayer, int yDistanceCurrentPlayer, int xFirstEmptyCell, int yFirstEmptyCell) DistanceCheck(int xCell, int yCell, int x, int y, CellState currentPlayer, CellState[,] board)
        {
            int xDistanceCurrentPlayer = 0, yDistanceCurrentPlayer = 0;
            int xFirstEmptyCell = 0; int yFirstEmptyCell = 0;

            for (int i = 0; xCell + x * i >= 0 &&
                xCell + x * i < n &&
                yCell + y * i >= 0 &&
                yCell + y * i < n; i++)
            {
                if (board[xCell + x * i, yCell + y * i] == currentPlayer)
                {
                    xDistanceCurrentPlayer = Math.Abs(x * i);
                    yDistanceCurrentPlayer = Math.Abs(y * i);
                }
                if ((board[xCell + x * i, yCell + y * i] == CellState.None ||
                    board[xCell + x * i, yCell + y * i] == CellState.Available) &&
                    xFirstEmptyCell == 0 &&
                    yFirstEmptyCell == 0)
                {
                    xFirstEmptyCell = Math.Abs(x * i);
                    yFirstEmptyCell = Math.Abs(y * i);
                }
            }
            //Geeft een zodanig grote x en y waarde aan de eerste lege cell variabelen voor als die er niet is, ofwel als er geen lege cellen worden aangetroffen dan krijgt het toch een waarde
            if (xFirstEmptyCell == 0 && yFirstEmptyCell == 0)
            {
                xFirstEmptyCell = 1000; yFirstEmptyCell = 1000;
            }
            return (xDistanceCurrentPlayer, yDistanceCurrentPlayer, xFirstEmptyCell, yFirstEmptyCell);
        }

        (int x, int y)[] CheckNeigbours(CellState otherPlayer, int xPos, int yPos, CellState[,] board)
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
                    if (board[xPos + x, yPos + y] == otherPlayer)
                        ValidNeighbouringCells.Add((x, y));
                }
            }
            return ValidNeighbouringCells.ToArray();
        }
        void ChangeBoardStatus(int xCell, int yCell, int x, int y, int xDistanceCurrentPlayer, int yDistanceCurrentPlayer, int xFirstEmptyCell, int yFirstEmptyCell, CellState[,] board)
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            for (int i = 0; xCell + x * i >= 0 &&
                    xCell + x * i < n &&
                    yCell + y * i >= 0 &&
                    yCell + y * i < n; i++)
            {
                bool firstCurrentPlayerCell = false;

                if (board[xCell + x * i, yCell + y * i] == currentPlayer &&
                    (xDistanceCurrentPlayer > Math.Abs(x * i) ||
                    yDistanceCurrentPlayer > Math.Abs(y * i)))
                    firstCurrentPlayerCell = true;

                if (board[xCell + x * i, yCell + y * i] == otherPlayer &&
                    (xFirstEmptyCell > xDistanceCurrentPlayer ||
                    yFirstEmptyCell > yDistanceCurrentPlayer) &&
                    (xDistanceCurrentPlayer > Math.Abs(x * i) ||
                    yDistanceCurrentPlayer > Math.Abs(y * i)))
                {
                    {
                        board[xCell, yCell] = currentPlayer;
                        ValidMove = true;
                    }
                    
                    if (firstCurrentPlayerCell == true)
                        break;
                    board[xCell + x * i, yCell + y * i] = currentPlayer;
                }
            }
        }
        void ResetAvailableCells(int xCell, int yCell, CellState[,] board)
        {
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (board[x, y] == CellState.Available)
                        board[x, y] = CellState.None;
                }
            }
        }
    }
}
