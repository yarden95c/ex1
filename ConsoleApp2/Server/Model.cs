using System;
using System.Collections.Generic;
using MazeLib;
using MazeGeneratorLib;
using System.Net.Sockets;

namespace Server
{
    internal class Model : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solutionsBFS;
        private Dictionary<string, Solution<Position>> solutionsDFS;
        //   private Dictionary<string, Maze> waitingMazes;
        private Dictionary<string, Game> startGames;
        private Dictionary<string, Game> playingGames;

        private ISearcher<Position> BFS;
        private ISearcher<Position> DFS;
        public Model()
        {
            playingGames = new Dictionary<string, Game>();
            startGames = new Dictionary<string, Game>();
            mazes = new Dictionary<string, Maze>();
            // waitingMazes = new Dictionary<string, Maze>();
            solutionsBFS = new Dictionary<string, Solution<Position>>();
            solutionsDFS = new Dictionary<string, Solution<Position>>();
            BFS = new BFS<Position>();
            DFS = new DFS<Position>();

        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        Maze IModel.GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = this.GetMaze(name, rows, cols);
            mazes.Add(name, maze);
            return maze;
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GetMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator generatorMaze = new DFSMazeGenerator();
            Maze maze = generatorMaze.Generate(rows, cols);
            maze.Name = name;
            return maze;
        }
        /// <summary>
        /// Gets the BFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Solution<Position> IModel.GetBFSSolution(string name)
        {
            if (solutionsBFS.ContainsKey(name))
            {
                return solutionsBFS[name];
            }
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);
            Solution<Position> solution = BFS.Search(mazeObjectAdapter);
            Console.WriteLine("BFS solution: ");
            return solution;
        }
        Solution<Position> IModel.GetDFSSolution(string name)
        {
            if (solutionsDFS.ContainsKey(name))
            {
                return solutionsDFS[name];
            }
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);
            Solution<Position> solution = DFS.Search(mazeObjectAdapter);
            Console.WriteLine("DFS solution: ");
            return solution;
        }

        /*  public void AddWaitingGame(string name, int rows, int cols)
           {
               Maze maze = this.GetMaze(name, rows, cols);
               waitingMazes.Add(name, maze);
           } 

        */
        public List<string> GetList()
        {
            List<string> namesList = new List<string>();
            foreach (string name in startGames.Keys)
            {
                namesList.Add(name);
            }
            return namesList;
        }
        public bool AddStartGame(Game game, string name)
        {
            if (!this.startGames.ContainsKey(name) && !this.playingGames.ContainsKey(name))
            {
                this.startGames.Add(name, game);
                return true;
            }
            else
            {
                //   Console.WriteLine("the game exist-give other name");
                return false;

            }
        }
        public Game FindGame(string name)
        {
           if (this.startGames.ContainsKey(name))
            {
                Game game = this.startGames[name];
                this.playingGames[name] = game;
                this.startGames.Remove(name);
                return game;
            }
            return null;
        }
        public void DeleteGameFromPlayingGames(string name)
        {
            this.playingGames.Remove(name);
        }
        public Game GetGame(string name)
        {
            return this.playingGames[name];
        }
        public Game FindGameByClient(TcpClient client)
        {
            foreach (Game game in this.playingGames.Values)
            {
                if (game.GetFirstClient() == client || game.GetSecondClient() == client)
                {
                    return game;
                }
            }
            return null;
        }
    }
}