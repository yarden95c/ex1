using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf
{
    public class AbstractClassModelClientServer
    {
        public string NameOfMaze
        {
            get { return Properties.Settings.Default.NameOfMaze; }
            set { Properties.Settings.Default.NameOfMaze = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }
        public string MazeString
        {
            get { return Properties.Settings.Default.MazeString; }
            set { Properties.Settings.Default.MazeString = value; }
        }
    }
}
