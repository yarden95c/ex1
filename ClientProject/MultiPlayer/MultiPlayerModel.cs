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
    class MultiPlayerModel : AbstractClassModelClientServer, INotifyPropertyChanged
    {
        Client client = null;
        private Position endPoint;
        private Position startPoint;
        private Position currentPointNew;
        private static MultiPlayerModel instance;
        private static Mutex instanceMutex = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private MultiPlayerModel()
        {
            this.client = new Client();
            this.client.EventOtherPlayerMove += delegate (string direction1)
            {
                this.UpdateCurrentPoint(direction1);
            };
        }
        private void UpdateCurrentPoint(string direction)
        {
            Position point = this.CurrentPointNew;
            switch (direction)
            {
                case "up":
                    point.Row--;
                    CurrentPointNew = point;
                    break;
                case "down":
                    point.Row++;
                    CurrentPointNew = point;
                    break;
                case "right":
                    point.Col++;
                    CurrentPointNew = point;
                    break;
                case "left":
                    point.Col--;
                    CurrentPointNew = point;
                    break;
            }
        }
        public static MultiPlayerModel Instance
        {
            get
            {
                instanceMutex.WaitOne();
                if (instance == null)
                {
                    instance = new MultiPlayerModel();
                }
                instanceMutex.ReleaseMutex();
                return instance;
            }
        }
        public Position CurrentPointNew
        {
            get { return this.currentPointNew; }
            set
            {
                this.currentPointNew = value;
                NotifyPropertyChanged("CurrentPointNew");
            }
        }
        public Position EndPoint
        {
            get { return this.endPoint; }
            set
            {
                this.endPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }
        public Position StartPoint
        {
            get { return this.startPoint; }
            set
            {
                this.startPoint = value;
                NotifyPropertyChanged("StartPoint");
                this.CurrentPointNew = value;
            }
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
            return null;
        }
    }
}
