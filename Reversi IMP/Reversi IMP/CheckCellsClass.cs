﻿using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
    internal partial class Program
    {
        public partial class Reversi : Form
        {
            void CheckCells()
            {
                if (CheckBoardValidity() == true)
                {
                    if (table[Row, Column] == CellState.None)
                    {
                        switch (move % 2)
                        {
                            case 0:
                                Check(CellState.Player1);
                                break;
                            case 1:
                                Check(CellState.Player2);
                                break;
                        }
                    }
                }
            }
            void Check(CellState i)
            {
                switch (CheckCorner())
                {
                    case -1: //niet in een hoek
                        Console.WriteLine("Niet in een hoek");
                        Console.WriteLine($"n: {n} Row: {Row} Column: {Column}");
                        break;
                    case 1: //linksboven
                        Console.WriteLine("LinksBoven");
                        break;
                    case 2: //Linksonder
                        Console.WriteLine("LinksOnder");
                        break;
                    case 3: //Rechtsboven
                        Console.WriteLine("RechtsBoven");
                        break;
                    case 4: //Rechtsonder
                        Console.WriteLine("RechtsOnder");
                        break;
                }
                table[Column, Row] = i;
                move++;
            }
            int CheckCorner()
            {
                if (Column == 0 && Row == 0)
                    return 1;
                else if (Column == 0 && Row == n - 1)
                    return 2;
                else if (Column == n - 1 && Row == 0)
                    return 3;
                else if (Column == n - 1 && Row == n - 1)
                    return 4;
                else return -1;
            }
            bool CheckBoardValidity()
            {
                for (int y = -1; y < 2 ; y++)
                {
                    for (int x = -1; x <2; x++)
                    {
                        if(Row + y == -1 || Row + y >= n || Column + x == -1 || Column + x >= n)
                        {
                            continue;
                        }
                        else
                        {
                            Console.Write(table[Row + y, Column + x]);
                            return false;
                        }
                    }
                    Console.WriteLine();
                }
                return false;
            }
        }
    }
}
