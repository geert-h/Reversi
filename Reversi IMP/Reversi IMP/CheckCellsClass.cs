using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi_IMP
{
    internal partial class Program
    {
        public partial class Reversi : Form
        {
            void CheckCells()
            {
                if (move % 2 == 0)
                {
                    if (table[Column, Row] == 1 || table[Column, Row] == 0)
                    {
                    }
                    else
                    {
                        table[Column, Row] = 1;

                        move++;
                    }

                }
                else
                {
                    if (table[Column, Row] == 1 || table[Column, Row] == 0)
                    {
                    }
                    else
                    {
                        table[Column, Row] = 0;
                        move++;
                    }
                }
            }
        }
    }
}
