using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        int drawCount = 0;
        void CheckGameState()
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

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
            Console.WriteLine($"None:{emptyCount}, Available: {availableCount}, Player1: {player1Count}, Player2: {player2Count}");

            if (availableCount == 0)
            {
                Console.WriteLine("Geen Zetten meer mogelijk");
                if (drawCount == 1)
                {
                    if(player1Count >  player2Count)
                    {
                        AvailabilityInfoLabel.Text = "Speler 1 heeft gewonnen.";
                    }
                    else if(player1Count == player2Count)
                    {
                        AvailabilityInfoLabel.Text = "Remise.";
                    }
                    else
                    {
                        AvailabilityInfoLabel.Text = "Speler 2 heeft gewonnen.";
                    }
                    AvailabilityButton.Show();
                    AvailabilityInfoLabel.Show();
                    AvailabilityBackgroundLabel.Show();
                    drawCount++;
                    move++;
                    CheckGameState();
                }
            }
        }
    }
}