using MazeLib;
using System.Threading;
using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
/// <summary>
/// 
/// </summary>
namespace ClientWpf
{
    class MultiPlayerModel : AbstractClassModelClientServer, INotifyPropertyChanged
    {
        Client client = null;
        private Position endPoint;
        private Position startPoint;
        private Position currentPointNew;
        private string notConnect;
        private static MultiPlayerModel instance;
        private static Mutex instanceMutex = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        /// Prevents a default instance of the <see cref="MultiPlayerModel"/> class from being created.
        /// </summary>
        private MultiPlayerModel()
        {
            this.client = new Client();
            this.client.EventOtherPlayerMove += delegate (string direction1)
            {
                this.UpdateCurrentPoint(direction1);
            };
            this.client.NotConnectWithServer += delegate ()
            {
               this.NotConnect = "NotConnect";
               // throw new Exception();
            };
    }
        /// <summary>
        /// Gets or sets the not connect.
        /// </summary>
        /// <value>
        /// The not connect.
        /// </value>
        public string NotConnect
        {
            get
            {
                return this.notConnect;
            }
            set
            {
                this.notConnect = value;
                NotifyPropertyChanged("NotConnect");
                
            }
        }
        /// <summary>
        /// Updates the current point.
        /// </summary>
        /// <param name="direction">The direction.</param>
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
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
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
        /// <summary>
        /// Gets or sets the current point new.
        /// </summary>
        /// <value>
        /// The current point new.
        /// </value>
        public Position CurrentPointNew
        {
            get { return this.currentPointNew; }
            set
            {
                this.currentPointNew = value;
                NotifyPropertyChanged("CurrentPointNew");
            }
        }
        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public Position EndPoint
        {
            get { return this.endPoint; }
            set
            {
                this.endPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }
        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
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
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public List<string> GetList()
        {
            try
            {
                string command = "list";
                string solution = this.GetCommand(command, false);
                return JsonConvert.DeserializeObject<List<string>>(solution);

            }
            catch (Exception)
            {
                return null;
              //  throw;
            }
        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns></returns>
        public string Start()
        {
            try
            {
                string command = "start " + this.NameOfMaze + " " + this.MazeRows + " " + this.MazeCols;
                string solution = this.GetCommand(command, false);
                Maze maze = Maze.FromJSON(solution);
                this.StartPoint = maze.InitialPos;
                this.EndPoint = maze.GoalPos;
                return solution;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Joins this instance.
        /// </summary>
        /// <returns></returns>
        public String Join() 
        {
            try
            {
                string command = "join " + this.NameOfMaze;
                string solution = this.GetCommand(command, false);
                Maze maze = Maze.FromJSON(solution);
                this.StartPoint = maze.InitialPos;
                this.EndPoint = maze.GoalPos;
                return solution;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Plays the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void Play(string move)
        {
            try
            {
                string command = "play " + move;
                this.GetCommand(command, true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <returns></returns>
        public string Close()
        {
            string command = "close " + this.NameOfMaze;
            string solution = this.GetCommand(command, true);
            return solution;
        }
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <returns></returns>
        private string GetCommand(string command, bool flag)
        {
            try
            {
                this.client.AddCommand(command);
                if (!flag)
                {
                    this.client.Connect();
                }
                if (!flag)
                    return this.client.GetAnswer();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Checks if close.
        /// </summary>
        public void CheckIfClose()
        {
            while (!this.client.StartMultyPlayerGame)
            {
                Thread.Sleep(100);
            }
            while (this.client.StartMultyPlayerGame)
            {
                Thread.Sleep(100);
            }
        }
    }
}
