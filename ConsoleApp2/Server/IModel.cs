using MazeLib;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

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
        void AddStartGame(Game game, string name);

        void DeleteGameFromPlayingGames(string name);
        Game FindGameByClient(TcpClient client);
        bool IsContainMazeForSolution(string name);
        bool IsGameAlreadyExist(string name);
        bool IsGameInWaitingList(string name);

        Game GetGameFromWaitingList(string name);
        void JoinToGame(string name);

        void StartAndPlayingMutexWaitOn();
        void StartAndPlayingMutexRealese();



    }
}