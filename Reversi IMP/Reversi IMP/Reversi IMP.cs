using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        Button StartButton = new Button(); Button HelpButton = new Button(); Button AvailabilityButton = new Button();

        Label AvailabilityBackgroundLabel = new Label(); Label AvailabilityInfoLabel = new Label();
        public static void Main()
        {
            Reversi reversi = new Reversi();
            reversi.Text = "Reversi";
            reversi.ClientSize = new Size(680, 980);
            Application.Run(reversi);
        }

        TextBox nTB = new TextBox();
        Label moveLBL = new Label();
        public int n = 6;
        public Point gridPoint = new Point(40, 340);
        int move = 0;
        int Column = 0; int Row = 0;
        bool help = false;
        CellState[,] table;

        void MakeButton(Button button, Size size, Point point, string text, bool show)
        {
            button.Size = size;
            button.Location = point;
            button.Text = text;
            Controls.Add(button);
            if (show)
                button.Show();
            else button.Hide();
        }
        void MakeTextBox(TextBox textbox, Size size, Point point, string text, bool show)
        {
            textbox.Size = size;
            textbox.Location = point;
            textbox.Text = text;
            Controls.Add(textbox);
            if (show)
                textbox.Show();
            else textbox.Hide();
        }
        void MakeLabel(Label label, Size size, Point point, string text, bool show)
        {
            label.Size = size;
            label.Location = point;
            label.Text = text;
            label.BackColor = Color.DarkSlateGray;
            Controls.Add(label);
            if (show)
                label.Show();
            else label.Hide();
        }

        public Reversi()
        {
            
            MakeButton(StartButton, new Size(100,25), new Point(20, 65), "Start", true);
            StartButton.Click += this.NnTB;

            MakeButton(HelpButton, new Size(100, 25), new Point(20, 95), "Help", true);
            HelpButton.Click += this.HButton;

            MakeTextBox(nTB, new Size(100, 20), new Point(20, 20), "6", true);

            MakeLabel(moveLBL, new Size(150, 20), new Point(120, 20), "Speler 1 is aan zet.", true);

            //NoAvailablePopUp
            MakeButton(AvailabilityButton, new Size(100, 25), new Point(260, 395), "OK", false);
            AvailabilityButton.Click += this.Availability;

            MakeLabel(AvailabilityBackgroundLabel, new Size(200, 200), new Point(240, 240), "", false);

            MakeLabel(AvailabilityInfoLabel, new Size(80, 20), new Point(260, 260), "", false);

            DoubleBuffered = true;
            this.Paint += this.GridDraw;;
            this.MouseClick += this.ClickMouse;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.LoadForm();

            this.Paint += this.DrawPieces;
        }

        void ClickMouse(object o, MouseEventArgs mea)
        {
            switch (move % 2)
            {
                case 0: moveLBL.Text = "Speler 1 is aan zet.";
                    break;
                case 1: moveLBL.Text = "Speler 2 is aan zet.";
                    break;
            }

            if(mea.X > gridPoint.X && 
               mea.X < gridPoint.X + 600 &&
               mea.Y > gridPoint.Y &&
               mea.Y < gridPoint.Y + 600)
            {
                int x = mea.X - gridPoint.X;
                int y = mea.Y - gridPoint.Y;

                Column = y / (600 / n);
                Row = x / (600 / n);

                CheckCells();
                CheckGameState();

                this.Invalidate();
            }
        }
        
        void Availability(object o, EventArgs ea)
        {
            move++;
            AvailabilityButton.Hide();
            AvailabilityBackgroundLabel.Hide();
            this.Invalidate();
        }
        void LoadForm()
        {
            table = new CellState[n, n];
            for (int i = 0; i < n * n; i++) 
                table[i % n, i / n] = CellState.None;

            Startpieces();
            CheckPossibleCells();
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
            CheckPossibleCells();

            this.Invalidate();
        }
        void HButton(object o, EventArgs ea)
        {
            if (!help) help = true;
            else help = false;
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
                        case CellState.None: break;
                        case CellState.Player1: pea.Graphics.FillEllipse(bR, xpos + offset, ypos + offset, size, size);
                            break;
                        case CellState.Player2: pea.Graphics.FillEllipse(bB, xpos + offset, ypos + offset, size, size);
                            break;
                        case CellState.Available: if(help) pea.Graphics.DrawEllipse(Pens.Black, xpos + offset + size / 4, ypos + offset + size / 4, size / 2, size / 2);
                            break;
                    }
                }
            }
        }
    }
}
