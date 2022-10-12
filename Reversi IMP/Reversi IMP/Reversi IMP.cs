using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Reversi_IMP
{
    internal partial class Program
    {
        public static void Main()
        {
            Reversi r = new Reversi();
            r.Text = "Reversi";
            r.ClientSize = new Size(680, 980);
            Application.Run(r);
        }

        public partial class Reversi : Form
        {
            TextBox nTB;
            int n = 6;
            int CellSize;
            Point gridPoint = new Point(40, 340);
            int xPos = 40; int yPos = 340;
            int move = 0;
            int Column = 0; int Row = 0;
            int[,] table;
            

            Button StartButton = new Button();


            void WriteLine()
            {
                if(move % 2 == 0)
                {
                    table[Column, Row] = 1;
                }
                else
                {
                    table[Column, Row] = 0;
                }

                for (int y = 0; y < n; y++)
                {
                    for (int x = 0; x < n; x++)
                    {
                        Console.Write(table[x, y]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            

            public Reversi()
            {
                nTB = new TextBox();
                nTB.Size = new Size(100, 20);
                nTB.Location = new Point(20, 20);
                Controls.Add(nTB);

                StartButton.Size = new Size(100, 25);
                StartButton.Location = new Point(20,65);
                StartButton.Text = "Start";
                Controls.Add(StartButton);

                DoubleBuffered = true;
                this.Paint += this.GridDraw;;
                this.MouseClick += this.ClickMouse;
                this.StartPosition = FormStartPosition.CenterScreen;

                this.LoadForm();

                this.Paint += this.DrawPieces;
            }

            void ClickMouse(object o, MouseEventArgs mea)
            {
                move++;

                if(mea.X > gridPoint.X && mea.X < gridPoint.X + 600 && mea.Y > gridPoint.Y && mea.Y < gridPoint.Y +600)
                {
                    double x = mea.X - gridPoint.X;
                    double y = mea.Y - gridPoint.Y;

                    Column = (int)(x / (600 / n));
                    Row = (int)(y / (600 / n));



                    xPos = gridPoint.X + Column * 600 / n;
                    yPos = gridPoint.Y + Row * 600 / n;

                    Console.WriteLine($"X:{Column}, Y:{Row} XMouse:{mea.X}, YMouse: {mea.Y}");
                    this.WriteLine();
                    this.Invalidate();
                     
                }
            }
            void LoadForm()
            {
                table = new int[n, n];
                for (int i = 0; i < n * n; i++) table[i % n, i / n] = -1;
            }
            void NnTB(object o, EventArgs ea)
            {
                n = Convert.ToInt32(nTB.Text);
                this.Invalidate();
            }

            void DrawPieces(object o, PaintEventArgs pea)
            {
                Brush b;
                int x = xPos;
                int y = yPos;
                int size = 600 / n - (600 / n) / 10;
                int offset = ((600 / n) / 10) / 2;
                if (move % 2 == 0)
                {
                    b = new SolidBrush(Color.Red);
                }
                else
                {
                    b = new SolidBrush(Color.Blue);
                }
                pea.Graphics.FillEllipse(b, x + offset, y + offset, size, size);
            }
        }
    }
}
