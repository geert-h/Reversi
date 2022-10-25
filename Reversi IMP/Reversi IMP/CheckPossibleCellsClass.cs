using System;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        //Gaat alle Cellen na om de Available cellen te bepalen
        public void CheckPossibleCells(CellState[,] board)
        {
            (CellState currentPlayer, CellState otherPlayer) = CurrentPlayer();

            for (int yCell = 0; yCell < n; yCell++)
            {
                for (int xCell = 0; xCell < n; xCell++)
                {
                    //Een cel die een waarde heeft van niet None wordt overgeslagen
                    if (board[xCell, yCell] != CellState.None)
                        continue;

                    foreach ((int x, int y) in CheckNeigbours(otherPlayer, xCell, yCell, board))
                    {
                        //Voor elke cel met een legale buur wordt de afstand tot de eerste cel van de huidige speler en de eerste lege cel bepaald
                        (int xDistanceCurrentPlayer, int yDistanceCurrentPlayer, int xFirstEmptyCell, int yFirstEmptyCell) = DistanceCheck(xCell, yCell, x, y, currentPlayer, board);

                        if (xDistanceCurrentPlayer == 0 &&
                            yDistanceCurrentPlayer == 0) continue;

                        //Checkt totdat een waarde niet meer in de array valt
                        for (int i = 1; xCell + x * i >= 0 &&
                            xCell + x * i < n &&
                            yCell + y * i >= 0 &&
                            yCell + y * i < n; i++)
                        {
                            //Als de cel van de andere speler is de afstand tot de eerste lege cel groter is dan de afstand tot de eerste cel van de huidige speler
                            //en de afstand van de huidige speler groter is dan de afstand tot de andere speler dan is het dus een geldige zet
                            if (board[xCell + x * i, yCell + y * i] == otherPlayer && 
                               (xFirstEmptyCell > xDistanceCurrentPlayer ||
                               yFirstEmptyCell > yDistanceCurrentPlayer) &&
                               (xDistanceCurrentPlayer > Math.Abs(x * i) ||
                               yDistanceCurrentPlayer > Math.Abs(y * i)))
                            {
                                board[xCell, yCell] = CellState.Available;
                            }
                        }
                    }
                }
            }
        }
    }
}
