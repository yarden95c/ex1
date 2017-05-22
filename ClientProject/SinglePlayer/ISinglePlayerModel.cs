using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf
{
    public interface ISinglePlayerModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        Position CurrentPoint { get; set; }
        string NameOfMaze { get; set; }
        int MazeCols { get; set; }
        int MazeRows { get; set; }
        string MazeString { get; set; }
        // string GenerateMaze();
        Position EndPoint { get; set; }
        Position StartPoint { get; set; }
        //string SolveMaze();
        //void DeleteSingleGame();
        //List<string> GetList();
        //string Start();
        //string Join();
        //void Play(string move);
        //string Close();
    }
}
