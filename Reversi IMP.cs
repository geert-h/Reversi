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
            Label moveLBL;
            public int n = 6;
            int CellSize;
            public Point gridPoint = new Point(40, 340);
            int move = 0;
            int Column = 0; int Row = 0;
            CellState[,] table;
            CellValidity[,] ValidityTable;

            Button StartButton = new Button();         

            public Reversi()
            {
                nTB = new TextBox();
                nTB.Size = new Size(100, 20);
                nTB.Location = new Point(20, 20);
                Controls.Add(nTB);

                moveLBL = new Label();
                moveLBL.Size = new Size(100, 20);
                moveLBL.Location = new Point(120, 20);
                moveLBL.Text = $"{move % 2}"; 
                Controls.Add(moveLBL);

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
                moveLBL.Text = Convert.ToString(move % 2); 
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
                table = new CellState[n, n];
                for (int i = 0; i < n * n; i++) table[i % n, i / n] = CellState.None;
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
                table = new CellState[n, n];
                for (int i = 0; i < n * n; i++) table[i % n, i / n] = CellState.None;
                move = 0;
                Startpieces();
                this.Invalidate();
            }
            void Startpieces()
            {
                table[(n / 2) - 1, (n / 2) - 1] = CellState.Player2;
                table[(n / 2), (n / 2) - 1] = CellState.Player1;
                table[(n / 2) - 1, (n / 2)] = CellState.Player1;
                table[(n / 2), (n / 2)] = CellState.Player2;
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
                        int xpos = gridPoint.X + x * 600 / n;
                        int ypos = gridPoint.Y + y * 600 / n;
                        switch (table[x, y])
                        {
                            case CellState.None:
                                break;
                            case CellState.Player1: pea.Graphics.FillEllipse(bR, xpos + offset, ypos + offset, size, size);
                                break;
                            case CellState.Player2: pea.Graphics.FillEllipse(bB, xpos + offset, ypos + offset, size, size);
                                break;
                            case CellState.Available: pea.Graphics.DrawEllipse(Pens.Black, xpos + offset, ypos + offset, size, size);
                                break;
                        }
                    }
                }
            }
        }
    }
}
