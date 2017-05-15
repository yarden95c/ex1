using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf
{
    class Location
    {
        private int row;
        private int col;
        public Location(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
    }
}
