using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
namespace Server
{
     class Game
    {
        TcpClient client;
        Maze maze;
        public Game(TcpClient client,Maze maze)
        {
            this.client = client;
            this.maze = maze;
        }
        public void Start()
        {
        
        }
    }
}
