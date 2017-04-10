using System;
using MazeLib;
using MazeGeneratorLib;
using System.Collections.Generic;
using SearchAlgorithmsLib;
using Ass1;

namespace ConsoleApp2
{
    internal class Model : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solutionsBFS;
        private Dictionary<string, Solution<Position>> solutionsDFS;
        private ISearcher<Position> BFS;
        private ISearcher<Position> DFS;
        public Model()
        {
            mazes = new Dictionary<string, Maze>();
            solutionsBFS = new Dictionary<string, Solution<Position>>();
            solutionsDFS = new Dictionary<string, Solution<Position>>();
            BFS = new BFS<Position>();
            DFS = new DFS<Position>();

        }



        Maze IModel.GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator generatorMaze = new DFSMazeGenerator();
            Maze maze = generatorMaze.Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, maze);
            return maze;
        }
        Solution<Position> IModel.getBFSSolution(string name)
        {
            if (solutionsBFS.ContainsKey(name)){
                return solutionsBFS[name];
            }
            ISearchable<Position> mazeObjectAdapter = new ObjectAdapter(mazes[name]);
            Solution<Position> solution = BFS.search(mazeObjectAdapter);
            Console.WriteLine("BFS solution: ");
            return solution;
        }

        public Solution<Position> getDFSSolution(string name)
        {
            throw new NotImplementedException();
        }
    }
}