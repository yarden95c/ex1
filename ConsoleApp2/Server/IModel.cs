
using MazeLib;
using System.Collections.Generic;
namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        Solution<Position> GetBFSSolution(string name);
        Solution<Position> GetDFSSolution(string name);
        void AddWaitingGame(string name, int rows, int cols);
        List<string> GetList();
    }
}