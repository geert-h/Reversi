using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        Button StartButton = new Button(); Button HelpButton = new Button(); Button AvailabilityButton = new Button(); Button AvailabilityNewGameButton = new Button();

        Label AvailabilityBackgroundLabel = new Label(); Label AvailabilityInfoLabel = new Label(); Label Player1Label = new Label(); Label Player2Label = new Label(); Label moveLBL = new Label(); Label BoardSizeLabel = new Label();

        TextBox BoardSizeTextBox = new TextBox();

        CheckBox AgainstBotCB = new CheckBox();

        public int n = 6; int move = 0; int Column = 0; int Row = 0;

        public Point gridPoint = new Point(40, 200);

        bool helpFunction = false; public bool ValidMove = false;

        CellState[,] table;

        Random rnd = new Random();

        public static void Main()
        {
            Reversi reversi = new Reversi();
            reversi.Text = "Reversi";
            reversi.ClientSize = new Size(680, 840);
            Application.Run(reversi);
        }

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
            Controls.Add(label);
            if (show)
                label.Show();
            else label.Hide();
        }

        public Reversi()
        {
            //NoAvailablePopUp
            MakeLabel(AvailabilityInfoLabel, new Size(150, 50), new Point(260, 260), "", false);

            MakeButton(AvailabilityNewGameButton, new Size(50, 25), new Point(365, 375), "Nieuw spel", false);
            AvailabilityNewGameButton.Click += this.NewBoardSize;

            MakeButton(AvailabilityButton, new Size(50, 25), new Point(265, 375), "OK", false);
            AvailabilityButton.Click += this.Availability;

            MakeLabel(AvailabilityBackgroundLabel, new Size(200, 200), new Point(240, 220), "", false);
            AvailabilityBackgroundLabel.BorderStyle = BorderStyle.FixedSingle;

            //Main Scherm functionaliteiten
            MakeButton(StartButton, new Size(100,25), new Point(290, 20), "Nieuw Spel", true);
            StartButton.Click += this.NewBoardSize;

            MakeButton(HelpButton, new Size(60, 25), new Point(310, 65), "Help", true);
            HelpButton.Click += this.HButton;

            MakeTextBox(BoardSizeTextBox, new Size(25, 25), new Point(615, 60), "6", true);
            BoardSizeTextBox.TextAlign = HorizontalAlignment.Right;

            MakeLabel(BoardSizeLabel, new Size(80, 25), new Point(540, 60), "Bord grootte", true);

            MakeLabel(moveLBL, new Size(150, 20), new Point(290, 170), "Speler 1 is aan zet.", true);
            moveLBL.ForeColor = Color.Red;

            MakeLabel(Player1Label, new Size(100, 20), new Point(110, 70), "2 Stenen", true);
            Player1Label.ForeColor = Color.Red;

            MakeLabel(Player2Label, new Size(100, 20), new Point(110, 140), "2 Stenen", true);
            Player2Label.ForeColor = Color.Blue;

            AgainstBotCB.Location = new Point(520, 20); AgainstBotCB.Size = new Size(120, 20); AgainstBotCB.Text = "Tegen bot spelen"; AgainstBotCB.CheckAlign = ContentAlignment.MiddleRight;
            AgainstBotCB.Checked = true;
            Controls.Add(AgainstBotCB);

            DoubleBuffered = true;
            this.Paint += this.GridDraw;;
            this.MouseClick += this.ClickMouse;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.LoadForm();

            this.Paint += this.DrawPieces;
        }

        void ClickMouse(object o, MouseEventArgs mea)
        {
            //Checkt voor een valide muispositie
            if (mea.X < gridPoint.X ||
               mea.X > gridPoint.X + 600 ||
               mea.Y < gridPoint.Y ||
               mea.Y > gridPoint.Y + 600)
                return;
            
            int x = mea.X - gridPoint.X;
            int y = mea.Y - gridPoint.Y;

            Column = y / (600 / n);
            Row = x / (600 / n);

            ValidMove = false;

            //Initieert de check op het bord, zodra deze valide is wordt direct de cel (Rij, Kolom) gezet naar de huidige speler, vervolgens worden de corresponderende cellen gefilpped
            CheckCells(Row, Column, table);

            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();
            
            //Als Checkcells een steen op het bord plaatst en de chechbox checked is dan doet de bot zijn zet
            if (ValidMove && AgainstBotCB.Checked)
            {
                move++;

                CheckPossibleCells(table);
                nthBestMove();
                CheckGameState();
                
                (int xOther, int yOther) = BestNextMove();

                //Als er geen geldige zetten meer zijn dan stop
                if (xOther == -1 && yOther == -1)
                    return;

                CheckCells(xOther, yOther, table);
                move++;
                CheckPossibleCells(table);
                CheckGameState();
                
            }
            
            //Telt de stenen om vervolgens dit weer te kunnen geven
            (int a, int b, int player1Count, int player2Count) = CountCells();

            string StoneCheck(int x)
            {
                if (x == 1)
                    return "Steen";
                else
                    return "Stenen";
            }

            Player1Label.Text = $"{Convert.ToString(player1Count)} {StoneCheck(player1Count)}";
            Player2Label.Text = $"{Convert.ToString(player2Count)} {StoneCheck(player2Count)}";

            //Checkt wie er aan de beurt is
            switch (move % 2)
            {
                case 0:
                    moveLBL.ForeColor = Color.Red;
                    moveLBL.Text = "Speler 1 is aan zet.";
                    break;
                case 1:
                    moveLBL.ForeColor = Color.Blue;
                    moveLBL.Text = "Speler 2 is aan zet.";
                    break;
            }

            this.Invalidate();
        }

        //Verbergt pop-up elementen van de gamestatus
        void Availability(object o, EventArgs ea)
        {
            move++;
            AvailabilityButton.Hide();
            AvailabilityNewGameButton.Hide();
            AvailabilityInfoLabel.Hide();
            AvailabilityBackgroundLabel.Hide();
            this.Invalidate();
        }

        //Laadt het programma in met de benodigde initiatie
        void LoadForm()
        {
            table = new CellState[n, n];
            for (int i = 0; i < n * n; i++) 
                table[i % n, i / n] = CellState.None;

            Startpieces();
            CheckPossibleCells(table);
        }

        //Voor het Click event van de nieuw bord grootte knop
        void NewBoardSize(object o, EventArgs ea)
        {
            //Controleert de juistheid van de ingevoegde waarde voor n
            try
            {
                n = Convert.ToInt32(BoardSizeTextBox.Text);
            }
            catch
            {
                n = 8;
            }

            if (n < 2)
                n = 2;

            for(;600 % n != 0; n--)
            {
                continue;
            }

            BoardSizeTextBox.Text = Convert.ToString(n);

            //Reset van het spel
            move = 0;

            Player1Label.Text = "2 Stenen";
            Player2Label.Text = "2 Stenen";

            table = new CellState[n, n];
            for (int i = 0; i < n * n; i++) table[i % n, i / n] = CellState.None;
            Startpieces();
            CheckPossibleCells(table);

            AvailabilityButton.Hide();
            AvailabilityNewGameButton.Hide();
            AvailabilityInfoLabel.Hide();
            AvailabilityBackgroundLabel.Hide();

            this.Invalidate();
        }

        void HButton(object o, EventArgs ea)
        {
            //toggle voor de hulp functie
            if (!helpFunction) helpFunction = true;
            else helpFunction = false;
            this.Invalidate();
        }

        void Startpieces()
        {
            //Plaatst de juiste start stenen
            table[(n / 2) - 1, (n / 2) - 1] = CellState.Player2;
            table[(n / 2), (n / 2) - 1] = CellState.Player1;
            table[(n / 2) - 1, (n / 2)] = CellState.Player1;
            table[(n / 2), (n / 2)] = CellState.Player2;
        }
        void DrawPieces(object o, PaintEventArgs pea)
        {
            //Tekent de de stenen op het n bij n bord
            int size = 600 / n - (600 / n) / 10;
            int offset = ((600 / n) / 10) / 2;
            Brush bR = new SolidBrush(Color.Red);
            Brush bB = new SolidBrush(Color.Blue);

            //Gaat elke combinatie Column en Row langs om de waardes te zetten
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
                        case CellState.Available: if(helpFunction) pea.Graphics.DrawEllipse(Pens.Black, xpos + offset + size / 4, ypos + offset + size / 4, size / 2, size / 2);
                            break;
                    }
                }
            }
            //Tekent de Stenen die altijd zichtbaar zijn
            pea.Graphics.FillEllipse(bR, 40, 45, 70, 70);
            pea.Graphics.FillEllipse(bB, 40, 115, 70, 70);
        }
    }
}
