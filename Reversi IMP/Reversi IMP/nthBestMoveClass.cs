using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        public int depth = 2;
        
        (int x, int y) nthBestMove()
        {
            return (0, 0);
        }

        (int x, int y) BestNextMove()
        {
            //Initiatie van een mirror van het bord (met deze mirror worden de berekeningen gedaan voor de bot)
            CellState[,] tablemirror = new CellState[n,n];
            tablemirror = (CellState[,])table.Clone();
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            //Benodigde lijsten voor het uitlezen van de beste zet
            List<(int xCoord, int yCoord, int cellCount)> AvailableCells = new List<(int xCoord, int yCoord, int cellCount)>();
            List<(int xCoord, int yCoord, int cellCount)> BestCells = new List<(int xCoord, int yCoord, int CellCount)>();
            int CellCount = 0;

            //Gaat elke Available cells langs om te controleren welke cell het beste resultaat oplevert
            for (int i = 0; i < tablemirror.Length; i++)
            {
                int xCell = i % n;
                int yCell = i / n;

                if (tablemirror[xCell, yCell] != CellState.Available)
                    continue;

                //Flipt de i'de available cell
                CheckCells(xCell, yCell, tablemirror);

                (int x, int y, int count) = RecursionTestCurrentPlayer(xCell, yCell, tablemirror, otherPlayer);

                //Telt het aantal cellen van de huidige speler en voegt deze vervolgens toe aan de lijst van alle available cellen
                CellCount = CountSpecificCells(currentPlayer, tablemirror);
                Console.WriteLine($"Coord:({xCell}, {yCell}), Amount: {CellCount}");
                AvailableCells.Add((xCell, yCell, CellCount));
                
                //Reset de tablemirror zodat de volgende cel gecheckt kan worden
                tablemirror = (CellState[,])table.Clone();
            }
            //Als er geen geldige waarde is dan wordt er een negatieve waarde teruggegeven zodat er duidelijk niets kan gebeuren
            if (AvailableCells.Count == 0) 
                return(-1,-1);

            //Neemt alle hoogste waardes uit alle available cellen en voegt deze toe aan de bestecellen lijst
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

            //Returned een willekeurige cel die het beste is
            int index = rnd.Next(BestCells.Count);

            Console.WriteLine($"Gekozen als beste: ({BestCells[index].xCoord}, {BestCells[index].yCoord}), Count: {BestCells[index].cellCount}");

            return (BestCells[index].xCoord, BestCells[index].yCoord);
        }


        (int x, int y, int count) RecursionTestCurrentPlayer(int xCell, int yCell, CellState[,] board, CellState currentPlayer)
        { 
            //Afkorting voor tablemirrormirror
            CellState[,] tmm = new CellState[n, n];
            tmm = (CellState[,])board.Clone();

            List<(int x, int y, int cellCount)> AvailableCells = new List<(int x, int y, int cellCount)>();
            List<(int x, int y, int cellCount)> BestCells = new List<(int x, int y, int cellCount)>();

            int CellCount;

            for (int i = 0; i < board.Length; i++)
            {
                CheckPossibleCells(board);

                int xsCell = i % n; //omdat xCell als in gebruik is maar xsCell (vanwege diepere laag check) idem voor yCell
                int ysCell = i / n;

                if (board[xsCell, ysCell] != CellState.Available)
                    continue;

                CheckCells(xsCell, ysCell, board);

                CellCount = CountSpecificCells(currentPlayer, board);

                Console.WriteLine($"sCoord:({xsCell}, {ysCell}), sAmount: {CellCount}");
                AvailableCells.Add((xsCell, ysCell, CellCount));

                tmm = (CellState[,])board.Clone();
            }
            if (AvailableCells.Count == 0)
                return (-1, -1, -1);

            int HighestCount = AvailableCells.Max(x => x.cellCount);

            foreach ((int xsCell, int ysCell, int Count) in AvailableCells)
            {
                if (Count > HighestCount)
                {
                    BestCells.Clear();
                    BestCells.Add((xsCell, ysCell, Count));
                }
                else if (Count == HighestCount)
                {
                    BestCells.Add((xsCell, ysCell, Count));
                }
            }

            //Returned een willekeurige cel die het beste is
            int index = rnd.Next(BestCells.Count);

            Console.WriteLine($"({BestCells[index].x}, {BestCells[index].y}), Count: {BestCells[index].cellCount}");

            return (BestCells[index].x, BestCells[index].y, BestCells[index].cellCount);
        }
        (int x, int y, int cellCount) RecursionTestOtherPlayer(int xCell, int yCell, CellState[,] board, CellState currentPlayer)
        {
            return (0, 0, 0);
        }
    }
}
