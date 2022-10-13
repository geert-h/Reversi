using System;
using System.Drawing;
using System.Windows.Forms;

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
            public int n = 6;
            int CellSize;
            public Point gridPoint = new Point(40, 340);
            int move = 0;
            int Column = 0; int Row = 0;
            int[,] table;
            

            Button StartButton = new Button();         

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
                StartButton.Click += this.NnTB;
                this.Paint += this.GridDraw;;
                this.MouseClick += this.ClickMouse;
                this.StartPosition = FormStartPosition.CenterScreen;

                this.LoadForm();

                this.Paint += this.DrawPieces;
            }

            void ClickMouse(object o, MouseEventArgs mea)
            {
                if(mea.X > gridPoint.X && mea.X < gridPoint.X + 600 && mea.Y > gridPoint.Y && mea.Y < gridPoint.Y + 600)
                {
                    double x = mea.X - gridPoint.X;
                    double y = mea.Y - gridPoint.Y;

                    Column = (int)(x / (600 / n));
                    Row = (int)(y / (600 / n));

                    CheckCells();
                    this.Invalidate();
                }
            }
            void LoadForm()
            {
                table = new int[n, n];
                for (int i = 0; i < n * n; i++) table[i % n, i / n] = -1;
                Startpieces();
            }
            void NnTB(object o, EventArgs ea)
            {
                n = Convert.ToInt32(nTB.Text);
                if (n % 2 == 1)
                {
                    n++;
                    nTB.Text = Convert.ToString(n);
                    
                }
                table = new int[n, n];
                for (int i = 0; i < n * n; i++) table[i % n, i / n] = -1;
                Startpieces();
                this.Invalidate();
            }
            void Startpieces()
            {
                table[(n / 2) - 1, (n / 2) - 1] = 1;
                table[(n / 2), (n / 2) - 1] = 0;
                table[(n / 2) - 1, (n / 2)] = 0;
                table[(n / 2), (n / 2)] = 1;
            }
            void DrawPieces(object o, PaintEventArgs pea)
            {
                int size = 600 / n - (600 / n) / 10;
                int offset = ((600 / n) / 10) / 2;
                Brush bR = new SolidBrush(Color.Red);
                Brush bB = new SolidBrush(Color.Blue);

                for (int y = 0; y < n; y++)
                {
                    for (int x = 0; x < n; x++)
                    {
                        Console.Write(table[x, y]);

                        int xpos = gridPoint.X + x * 600 / n;
                        int ypos = gridPoint.Y + y * 600 / n;
                        switch (table[x, y])
                        {
                            case -1:break;
                            case 0: pea.Graphics.FillEllipse(bR, xpos + offset, ypos + offset, size, size);
                                break;
                            case 1: pea.Graphics.FillEllipse(bB, xpos + offset, ypos + offset, size, size);
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
