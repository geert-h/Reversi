using System.Drawing;
using System.Windows.Forms;

namespace Reversi_IMP
{
    public partial class Reversi : Form
    {
        int CellSize;

        void GridDraw(object o, PaintEventArgs pea)
        {
            SolidBrush BackgroundBrush = new SolidBrush(Color.FromArgb(64, 128, 0));
            pea.Graphics.FillRectangle(BackgroundBrush, gridPoint.X, gridPoint.Y, 600, 600);
            CellSize = 600 / n;
            for (int y = 0; y <= n; y++)
            {
                Pen pen = new Pen(Color.Black, 2);
                pea.Graphics.DrawLine(pen, gridPoint.X, gridPoint.Y + y * CellSize, gridPoint.X + n * CellSize, gridPoint.Y + y * CellSize);
            }
            for (int x = 0; x <= n; x++)
            {
                Pen pen = new Pen(Color.Black, 2);
                pea.Graphics.DrawLine(pen, gridPoint.X + x * CellSize, gridPoint.Y, gridPoint.X + x * CellSize, gridPoint.Y + n * CellSize);
            }
        }
    }
}
