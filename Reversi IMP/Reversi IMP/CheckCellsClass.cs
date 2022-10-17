using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
        public partial class Reversi : Form
        {
        void CheckCells()
        {
            if (table[Row, Column] == CellState.None)
            {
                if (CheckBoardValidity() == true)
                {
                    NeighbourCheck();
                }
            }
        }

        bool CheckBoardValidity()
        {
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (Row + x > 0 && Row + x < n && Column + y > 0 && Column + y < n)
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
            switch (move % 2)
            {
                case 0:
                    if (ValidMoveCheck(CellState.Player1, CellState.Player2) == true)
                    {
                        table[Row, Column] = CellState.Player1;
                        
                        move++;
                    }
                    break;
                case 1:
                    if (ValidMoveCheck(CellState.Player2, CellState.Player1) == true)
                    {
                        table[Row, Column] = CellState.Player2;
                        move++;
                    }
                    break;
            }
        }

        bool ValidMoveCheck(CellState player1, CellState player2)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x != 0 || y != 0)
                    {
                        if (Check(player1, player2, x, y) == true)
                            return true;
                    }
                }
            }

            bool Check(CellState player1, CellState player2, int x, int y)
            {
                int Xplayer1 = 0; int Yplayer1 = 0;
                int Xplayer2 = 0; int Yplayer2 = 0;
                int Xnone = 0; int Ynone = 0;
                bool player1Cell = false; bool player2Cell = false;

                if (Math.Abs(x) == Math.Abs(y))
                {
                    DiagonalCheck();
                }
                else
                    StraightCheck();

                Console.WriteLine($"P1: ({Xplayer1}, {Yplayer1}), P2: ({Xplayer2}, {Yplayer2}) V: {(Xplayer1 != 0 || Yplayer1 != 0) && (Xplayer2 != 0 || Yplayer2 != 0)}");

                if ((Xplayer1 != 0 || Yplayer1 != 0) && (Xplayer2 != 0 || Yplayer2 != 0))
                {
                    if ((Xplayer2 <= Xplayer1) && (Yplayer2 <= Yplayer1))
                    {
                        Console.WriteLine($"RIJ: {Row}, KOLOM: {Column}, x: {x}, y: {y}");
                        return true;
                    }
                }
                return false;

                void StraightCheck()
                {
                    for (int i = 0; Row + x * i < n && Row + x * i >= 0; i++)
                    {
                        for (int j = 0; Column + y * j < n && Column + y * j >= 0; j++)
                        {
                            CheckCheck(i, j);
                            if (y == 0)
                                break;
                        }
                        if (x == 0)
                            break;
                    }
                }
                void DiagonalCheck()
                {
                    for (int i = 0; Row + x * i < n && Row + x * i >= 0 && Column + y * i < n && Column + y * i >= 0; i++)
                    {
                        CheckCheck(i, i);
                    }

                }

                void CheckCheck(int i, int j)
                {
                    if (table[Row + x * i, Column + y * j] == player2)
                    {
                        Xplayer2 = Math.Abs(x * i); Yplayer2 = Math.Abs(y * j);
                        //Console.Write($"x2: {Xplayer2}, y2: {Yplayer2}");
                    }
                    else if (table[Row + x * i, Column + y * j] == player1)
                    {
                        if (player1Cell == false)
                        {
                            Xplayer1 = Math.Abs(x * i); Yplayer1 = Math.Abs(y * j);
                            player1Cell = true;
                        }

                        //Console.Write($"(x1: {Xplayer1}, y1; {Yplayer1})");
                    }
                    else if (table[Row + x * i, Column + y * j] == CellState.None)
                    {
                        Xnone = Math.Abs(x * i); Ynone = Math.Abs(y * j);
                        //Console.Write(table[Row + x * i, Column + y * j]);
                    }

                    if (Xplayer1 != 0 || Yplayer1 != 0)
                    {
                        FlipPieces(i, j);
                    }
                }
                void FlipPieces(int i, int j)
                {
                    if (player1Cell == true)
                    {
                        if (Math.Abs(x) == Math.Abs(y))
                        {
                            for (int k = 0; table[Row + x * i - k, Column + y * j - k] != table[Row, Column]; k++)
                            {
                                Console.WriteLine($"{Row + x * i - k}, {Column + y * j - k}");
                                if (table[Row + x * i - k, Column + y * j - k] == player2)
                                {
                                    table[Row + x * i - k, Column + y * j - k] = player1;
                                }
                            }
                        }
                        if (x == 0)
                        {
                            for (int k = 0; Column + y * j - k != Column; k++)
                            {
                                Console.WriteLine(Row + x * i - k);
                                if (table[Row, Column + y * j - k] == player2)
                                {
                                    table[Row, Column + y * j - k] = player1;
                                }
                            }
                        }
                        if (y == 0)
                        {
                            for (int l = 0; Row + x * i - l != Row; l++)
                            {
                            Console.WriteLine(Row + x * i - l);
                                if (table[Row + x * i - l, Column] == player2)
                                {
                                    table[Row + x * i - l, Column] = player1;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
