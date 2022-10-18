using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        public static void Main()
        {
            Reversi r = new Reversi();
            r.Text = "Reversi";
            r.ClientSize = new Size(680, 980);
            Application.Run(r);
        }

        TextBox nTB;
        Label moveLBL;
        public int n = 6;
        int CellSize;
        public Point gridPoint = new Point(40, 340);
        int move = 0;
        int Column = 0; int Row = 0;
        CellState[,] table;

        Button StartButton = new Button();         

        public Reversi()
        {
            nTB = new TextBox();
            nTB.Size = new Size(100, 20);
            nTB.Location = new Point(20, 20);
            nTB.Text = "6";
            Controls.Add(nTB);

            moveLBL = new Label();
            moveLBL.Size = new Size(100, 20);
            moveLBL.Location = new Point(120, 20);
            nTB.Text = "6";
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
            switch(move % 2)
            {
                case 0: moveLBL.Text = Convert.ToString(CellState.Player1);
                    break;
                case 1: moveLBL.Text = Convert.ToString(CellState.Player2);
                    break;
            }

            if(mea.X > gridPoint.X && mea.X < gridPoint.X + 600 && mea.Y > gridPoint.Y && mea.Y < gridPoint.Y + 600)
            {
                double x = mea.X - gridPoint.X;
                double y = mea.Y - gridPoint.Y;

                Column = (int)(y / (600 / n));
                Row = (int)(x / (600 / n));

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
            move = 0;
            if (n % 2 == 1)
            {
                n++;
                nTB.Text = Convert.ToString(n);
                    
            }
            table = new CellState[n, n];
            for (int i = 0; i < n * n; i++) table[i % n, i / n] = CellState.None;
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
                    Console.Write(table[x, y]);

                    int xpos = gridPoint.X + x * 600 / n;
                    int ypos = gridPoint.Y + y * 600 / n;
                    switch (table[x, y])
                    {
                        case CellState.None: break;
                        case CellState.Player1: pea.Graphics.FillEllipse(bR, xpos + offset, ypos + offset, size, size);
                            break;
                        case CellState.Player2: pea.Graphics.FillEllipse(bB, xpos + offset, ypos + offset, size, size);
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
