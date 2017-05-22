using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ClientWpf.MultiPlayer
{
    public class MultiPlayerModel
    {
        private Position endPoint;
        private Position startPoint;
        private Position currentPoint;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
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
        public Position EndPoint
        {
            get { return this.endPoint; }
            set { this.endPoint = value; }
        }
        public Position StartPoint
        {
            get { return this.startPoint; }
            set
            {
                this.startPoint = value;
                this.CurrentPoint = value;
            }
        }
        public Position CurrentPoint
        {
            get { return this.currentPoint; }
            set
            {
                this.currentPoint = value;
             //   NotifyPropertyChanged("CurrentPoint");
            }
        }

    }
}
