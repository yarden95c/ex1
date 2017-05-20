using MazeLib;
using System.Threading;
using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ClientWpf
{
    class SinglePlayerModel : ISinglePlayerModel
    {
        Client client = null;
        //        private ObservableCollection<string> games = new ObservableCollection<string>();
        private Position endPoint;
        private Position startPoint;
        private static SinglePlayerModel instance;
        private static Mutex instanceMutex = new Mutex();
        private SinglePlayerModel()
        {
            this.client = new Client();
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
        public string NameOfMaze
        {
            get { return Properties.Settings.Default.NameOfMaze; }
            set { Properties.Settings.Default.NameOfMaze = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRowsSinglePlayer; }
            set { Properties.Settings.Default.MazeRowsSinglePlayer = value; }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeColsSinglePlayer; }
            set { Properties.Settings.Default.MazeColsSinglePlayer = value; }
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
            set { this.startPoint = value; }
        }
        public string GenerateMaze()
        {
            string command = "generate" + " " + this.NameOfMaze + " "
                + this.MazeRows + " " + this.MazeCols;
            string maze = this.GetCommand(command);
            return maze;
        }
        public string SolveMaze()
        {
            int algo = Properties.Settings.Default.SearchAlgorithm;
            string command = "solve" + " " + this.NameOfMaze + " "
                + algo.ToString();
            string solution = this.GetCommand(command);
            return solution;
        }
        public string StartMaze()
        {
            string command = "start" + " " + this.NameOfMaze + " " + this.MazeRows + " " + this.MazeCols;
            string solution = this.GetCommand(command);
            return solution;
        }
        public List<string> GetList()
        {
            string command = "list";
            string solution = this.GetCommand(command);
            return JsonConvert.DeserializeObject<List<string>>(solution);
        }
        public void Start()
        {
            string command = "start" + " " + this.NameOfMaze + " " + this.MazeRows + " " + this.MazeCols;
            this.GetCommand(command);
        }
        public void Join()
        {
            string command = "join" + " " + this.NameOfMaze;
            this.GetCommand(command);
        }
        private string GetCommand(string command)
        {
            this.client.AddCommand(command);
            this.client.Connect();
            return this.client.GetAnswer();
        }

        public void DeleteSingleGame()
        {
            string command = "delete" + " " + this.NameOfMaze;
            string solution = this.GetCommand(command);

        }
    }
}
