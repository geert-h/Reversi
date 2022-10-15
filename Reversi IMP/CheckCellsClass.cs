using System;
using System.DirectoryServices.ActiveDirectory;
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
                    NeighbourCheck();
                };
            }

            bool CheckBoardValidity()
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        if(Row + x > 0 && Row + x < n && Column + y > 0 && Column + y < n)
                        {
                            if ((table[Row + x, Column + y] == CellState.Player1 || table[Row + x, Column + y] == CellState.Player2))
                                return true;
                        }
                    }
                }
                return false;
            }

            void NeighbourCheck()
            {
                switch(move % 2)
                {
                    case 0:
                        //if (table[Column + x, Row + y] == CellState.Player2)
                        //{
                            if (ValidMoveCheck(CellState.Player1, CellState.Player2) == true)
                            {
                                table[Column, Row] = CellState.Player1;
                                move++;
                            }
                            
                        //}
                        break;
                    case 1:
                        //if (table[Row + x, Column + y] == CellState.Player1)
                        //{
                            if (ValidMoveCheck(CellState.Player2, CellState.Player1) == true)
                            {
                                table[Column, Row] = CellState.Player2;
                                move++;
                            }
                        //}
                        break;
                }
            }

            bool ValidMoveCheck(CellState player, CellState player2)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        if (x != 0 || y != 0)
                        {
                            if (Check(player, player2, x, y) == true)
                                return true;
                        }
                    }
                }
                
                bool Check(CellState player, CellState player2, int x, int y)
                {
                    int Xplayer1 = 0; int Yplayer1 = 0;
                    int Xplayer2 = 0; int Yplayer2 = 0;
                    int Xnone = 0; int Ynone = 0;

                    for(int i = 0; Row + x * i < n && Row + x * i >= 0; i++)
                    {
                        for (int j = 0; Column + y * j < n && Column + y * j >= 0; j++)
                        {
                            if (table[Row + x * i, Column + y * j] == player2)
                            {
                                Xplayer2 = Math.Abs(x * i); Yplayer2 = Math.Abs(y * j);
                            }
                            else if (table[Row + x * i, Column + y * j] == player)
                            {
                                Xplayer1 = Math.Abs(x * i); Yplayer1 = Math.Abs(y * j);
                            }
                            else if (table[Row + x * i, Column + y * j] == CellState.None)
                            {
                                Xnone = Math.Abs(x * i); Ynone = Math.Abs(y * j);
                            }
                            if (y == 0)
                                break;
                        }
                        if (x == 0)
                            break;
                    }
                    if (Xplayer1 != 0 || Yplayer1 != 0)
                    {
                            if ((Xplayer1 <= Xplayer2) && (Yplayer1 <= Yplayer2))
                            {
                                Console.WriteLine($"RIJ: {Row}, KOLOM: {Column}");
                                return true;
                            }
                    }
                    Console.WriteLine($"1({Xplayer1}, {Yplayer1}), 2({Xplayer2}, {Yplayer2})");
                    return false;
                }
                Console.WriteLine();
                return false;
            }
        }
    }
}