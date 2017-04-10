using MazeLib;
using SearchAlgorithmsLib;

namespace ConsoleApp2
{
    public interface IModel
    {
        Maze GenerateMaze(string name,int rows, int cols);
        Solution<Position> getBFSSolution(string name);
        Solution<Position> getDFSSolution(string name);



    }
}