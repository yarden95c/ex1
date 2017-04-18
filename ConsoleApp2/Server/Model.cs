using System;
using System.Collections.Generic;
using MazeLib;
using MazeGeneratorLib;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class Model : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solutionsBFS;
        private Dictionary<string, Solution<Position>> solutionsDFS;
        private Dictionary<string, Game> startGames;
        private Dictionary<string, Game> playingGames;
        private static Mutex mazesMutex = new Mutex();
        private static Mutex bfsMutex = new Mutex();
        private static Mutex dfsMutex = new Mutex();
        private static Mutex startMutex = new Mutex();
        private static Mutex playingMutex = new Mutex();

        private ISearcher<Position> BFS;
        private ISearcher<Position> DFS;
        public Model()
        {
            playingGames = new Dictionary<string, Game>();
            startGames = new Dictionary<string, Game>();
            mazes = new Dictionary<string, Maze>();
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
            mazesMutex.WaitOne();
            Maze maze = this.GetMaze(name, rows, cols);
            mazes.Add(name, maze);
            mazesMutex.ReleaseMutex();
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
            bfsMutex.WaitOne();
            if (solutionsBFS.ContainsKey(name))
            {
                return solutionsBFS[name];
            }
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);
            Solution<Position> solution = BFS.Search(mazeObjectAdapter);
            bfsMutex.ReleaseMutex();
            return solution;
        }
        Solution<Position> IModel.GetDFSSolution(string name)
        {
            dfsMutex.WaitOne();
            if (solutionsDFS.ContainsKey(name))
            {
                return solutionsDFS[name];
            }
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);
            Solution<Position> solution = DFS.Search(mazeObjectAdapter);
            Console.WriteLine("DFS solution: ");
            dfsMutex.ReleaseMutex();
            return solution;
        }
        public bool IsContainMazeForSolution(string name)
        {
            mazesMutex.WaitOne();
            if (mazes.ContainsKey(name))
            {
                mazesMutex.ReleaseMutex();
                return true;
            }
            mazesMutex.ReleaseMutex();
            return false;
        }

        public List<string> GetList()
        {
            startMutex.WaitOne();
            List<string> namesList = new List<string>();
            foreach (string name in startGames.Keys)
            {
                namesList.Add(name);
            }
            startMutex.ReleaseMutex();
            return namesList;
        }
        public void AddStartGame(Game game, string name)
        {
            startGames.Add(name, game);
        }
        public bool IsGameAlreadyExist(string name)
        {
            return (this.startGames.ContainsKey(name) || this.playingGames.ContainsKey(name));
        }
        bool IsGameInWaitingList(string name)
        {
            return startGames.ContainsKey(name);
        }
        public void JoinToGame(string name)
        {
            Game game = this.startGames[name];
            this.playingGames[name] = game;
            this.startGames.Remove(name);

        }
        public Game GetGameFromWaitingList(string name)
        {
            return this.startGames[name]; ;
        
        }
        public void DeleteGameFromPlayingGames(string name)
        {
            playingMutex.WaitOne();
            this.playingGames.Remove(name);
            playingMutex.ReleaseMutex();
        }

        public Game FindGameByClient(TcpClient client)
        {
            playingMutex.WaitOne();
            foreach (Game game in this.playingGames.Values)
            {
                if (game.GetFirstClient() == client || game.GetSecondClient() == client)
                {
                    playingMutex.ReleaseMutex();
                    return game;
                }
            }
            playingMutex.ReleaseMutex();
            return null;
        }

        public void StartAndPlayingMutexWaitOn()
        {
            startMutex.WaitOne();
            playingMutex.WaitOne();
        }

        public void StartAndPlayingMutexRealese()
        {
            startMutex.ReleaseMutex();
            playingMutex.ReleaseMutex();
        }

        bool IModel.IsGameInWaitingList(string name)
        {
            return startGames.ContainsKey(name);
        }
    }
}