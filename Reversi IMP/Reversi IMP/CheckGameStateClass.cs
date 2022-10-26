using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        void CheckGameState()
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            (int emptyCount, int availableCount, int player1Count, int player2Count) = CountCells();

            if (availableCount == 0)
            {
                move++;
                (emptyCount, availableCount, player1Count, player2Count) = CountCells();

                if(availableCount != 0)
                {
                    move--;
                    string huidigeSpeler;
                    if (currentPlayer == CellState.Player1)
                        huidigeSpeler = "Speler 1";
                    else huidigeSpeler = "Speler 2";

                    AvailabilityInfoLabel.Text = $"Geen mogelijke zetten voor {huidigeSpeler}.";
                }
                else
                {
                    if (player1Count >  player2Count)
                    {
                        AvailabilityInfoLabel.Text = "Speler 1 heeft gewonnen.";
                    }
                    else if (player1Count == player2Count)
                    {
                        AvailabilityInfoLabel.Text = "Remise.";
                    }
                    else
                    {
                        AvailabilityInfoLabel.Text = "Speler 2 heeft gewonnen.";
                    }
                }

                AvailabilityButton.Show();
                AvailabilityNewGameButton.Show();
                AvailabilityInfoLabel.Show();
                AvailabilityBackgroundLabel.Show();
            }
        }
        (int empty, int available, int player1, int player2) CountCells()
        {
            int player1Count = 0;
            int player2Count = 0;
            int emptyCount = 0;
            int availableCount = 0;

            for (int i = 0; i < table.Length; i++)
            {
                switch (table[i % n, i / n])
                {
                    case CellState.None:
                        emptyCount++;
                        break;
                    case CellState.Available:
                        availableCount++;
                        break;
                    case CellState.Player1:
                        player1Count++;
                        break;
                    case CellState.Player2:
                        player2Count++;
                        break;
                }
            }
            return (emptyCount, availableCount, player1Count, player2Count);
        }
        int CountSpecificCells(CellState specificCellState, CellState[,] board)
        {
            int SpecificCellCount = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if(board[i % n, i / n] == specificCellState)
                {
                    SpecificCellCount++;
                }
            }
            return SpecificCellCount;
        }
    }
}