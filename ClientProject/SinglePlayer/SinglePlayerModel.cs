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
    class SinglePlayerModel : AbstractClassModelClientServer
    {
        Client client = null;
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
            }
        }
        public string GenerateMaze()
        {
            try
            {
                string command = "generate " + this.NameOfMaze + " "
                    + this.MazeRows + " " + this.MazeCols;
                string maze = this.GetCommand(command, false);
                return maze;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string SolveMaze()
        {
            try
            {
                int algo = Properties.Settings.Default.SearchAlgorithm;
                string command = "solve " + this.NameOfMaze + " "
                    + algo.ToString();
                string solution = this.GetCommand(command, false);
                return solution;
            }
            catch (Exception)
            {

                throw;
            }
        }
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

        public void DeleteSingleGame()
        {
            try
            {
                string command = "delete " + this.NameOfMaze;
                string solution = this.GetCommand(command, false);
            }
            catch (Exception)
            {
            }
        }
    }
}
