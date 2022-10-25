using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        
        (int x, int y) nthBestMove()
        {
            return (0, 0);
        }

        (int x, int y) BestNextMove()
        {
            CellState[,] tablemirror = new CellState[n,n];
            tablemirror = (CellState[,])table.Clone();
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();
            List<(int xCoord, int yCoord, int cellCount)> AvailableCells = new List<(int xCoord, int yCoord, int cellCount)>();
            List<(int xCoord, int yCoord, int cellCount)> BestCells = new List<(int xCoord, int yCoord, int CellCount)>();
            int CellCount = 0;

            for (int i = 0; i < tablemirror.Length; i++)
            {
                int xCell = i % n;
                int yCell = i / n;

                if (tablemirror[xCell, yCell] != CellState.Available)
                    continue;

                CheckCells(xCell, yCell, tablemirror);

                CellCount = CountSpecificCells(currentPlayer);

                Console.WriteLine($"Coord:({xCell}, {yCell}), Amount: {CellCount}");
                AvailableCells.Add((xCell, yCell, CellCount));

                tablemirror = (CellState[,])table.Clone();
            }

            if (AvailableCells.Count == 0) 
                return(-1,-1);

            int HighestCount = AvailableCells.Max(x => x.cellCount);
            
            foreach ((int xCell, int yCell, int Count) in AvailableCells)
            {
                if (Count > HighestCount)
                {
                    BestCells.Clear();
                    BestCells.Add((xCell, yCell, Count));
                }
                else if (Count == HighestCount)
                {
                    BestCells.Add((xCell, yCell, Count));
                }
            }

            int index = rnd.Next(BestCells.Count);

            Console.WriteLine($"({BestCells[index].xCoord}, {BestCells[index].yCoord}), Count: {BestCells[index].cellCount}");

            return (BestCells[index].xCoord, BestCells[index].yCoord);
        }

    }
}
