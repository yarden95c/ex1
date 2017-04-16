using MazeLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        Solution<Position> GetBFSSolution(string name);
        Solution<Position> GetDFSSolution(string name);
        //   void AddWaitingGame(string name, int rows, int cols);
        List<string> GetList();
        Maze GetMaze(string name, int rows, int cols);
        bool AddStartGame(Game game, string name);
        Game FindGame(string name);
        void DeleteGameFromPlayingGames(string name);
        Game GetGame(string name);
        Game FindGameByClient(TcpClient client);

    }
}