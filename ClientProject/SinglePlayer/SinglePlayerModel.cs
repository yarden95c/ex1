using MazeLib;
using System.Threading;
using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
namespace ClientWpf
{
    class SinglePlayerModel : ISinglePlayerModel, INotifyPropertyChanged
    {
        Client client = null;
        private Position endPoint;
        private Position startPoint;
        private string direction;
        private Position currentPoint;
        private static SinglePlayerModel instance;
        private static Mutex instanceMutex = new Mutex();

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private SinglePlayerModel()
        {
            this.client = new Client();
            this.client.EventOtherPlayerMove += delegate (string direction1)
            {
                // this.Direction = "abra kadabra";
                this.UpdateCurrentPoint(direction1);
                //    Console.WriteLine(direction);
                // this.Direction = null;
            };
        }
        private void UpdateCurrentPoint(string direction)
        {
            Position point = this.CurrentPoint;
            switch (direction)
            {
                case "up":
                    point.Row--;
                    CurrentPoint = point;
                    break;
                case "down":
                    point.Row++;
                    CurrentPoint = point;
                    break;
                case "right":
                    point.Col++;
                    CurrentPoint = point;
                    break;
                case "left":
                    point.Col--;
                    CurrentPoint = point;
                    break;
            }
        }
        public static SinglePlayerModel Instance
        {
            get
            {
                instanceMutex.WaitOne();
                if (instance == null)
                {
                    instance = new SinglePlayerModel();
                }
                instanceMutex.ReleaseMutex();
                return instance;
            }
        }
        /*  public string Direction
          {
              get { return this.direction; }
              set
              {
                  this.direction = value;
                  NotifyPropertyChanged("Direction");
              }
          }*/
        public Position CurrentPoint
        {
            get { return this.currentPoint; }
            set
            {
                this.currentPoint = value;
                NotifyPropertyChanged("CurrentPoint");
            }
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
        public string GenerateMaze()
        {
            string command = "generate " + this.NameOfMaze + " "
                + this.MazeRows + " " + this.MazeCols;
            string maze = this.GetCommand(command, false);
            return maze;
        }
        public string SolveMaze()
        {
            int algo = Properties.Settings.Default.SearchAlgorithm;
            string command = "solve " + this.NameOfMaze + " "
                + algo.ToString();
            string solution = this.GetCommand(command, false);
            return solution;
        }
        public List<string> GetList()
        {
            string command = "list";
            string solution = this.GetCommand(command, false);
            return JsonConvert.DeserializeObject<List<string>>(solution);
        }
        public string Start()
        {
            string command = "start " + this.NameOfMaze + " " + this.MazeRows + " " + this.MazeCols;
            string solution = this.GetCommand(command, false);
            Maze maze = Maze.FromJSON(solution);
            this.StartPoint = maze.InitialPos;
            this.EndPoint = maze.GoalPos;
            return solution;
        }
        public String Join()
        {
            string command = "join " + this.NameOfMaze;
            string solution = this.GetCommand(command, false);
            Maze maze = Maze.FromJSON(solution);
            this.StartPoint = maze.InitialPos;
            this.EndPoint = maze.GoalPos;
            return solution;
        }
        public void Play(string move)
        {
            string command = "play " + move;
            this.GetCommand(command, true);
        }
        public string Close()
        {
            string command = "close " + this.NameOfMaze;
            string solution = this.GetCommand(command, true);
            return solution;
        }
        private string GetCommand(string command, bool flag)
        {
            if (!flag)
            {
                this.client.Connect();
            }
            this.client.AddCommand(command);
            if (!flag)
                return this.client.GetAnswer();
            else
                return "Aa";
        }

        public void DeleteSingleGame()
        {
            string command = "delete " + this.NameOfMaze;
            string solution = this.GetCommand(command, false);

        }
    }
}
