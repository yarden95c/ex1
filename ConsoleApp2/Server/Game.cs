using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using System.IO;

namespace Server
{
    public class Game
    {
        private TcpClient client1;
        private TcpClient client2;
        private Maze maze;
        public Game(TcpClient client, Maze maze)
        {
            this.client1 = client;
            this.maze = maze;
        }
        public void Join(TcpClient client)
        {
            this.client2 = client;
            this.SendToClients();

        }
        public TcpClient GetFirstClient()
        {
            return this.client1;
        }
        public TcpClient GetSecondClient()
        {
            return this.client2;
        }
        public void SendToClients()
        {
            NetworkStream stream = client1.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(maze.ToJSON());
            writer.Flush();
            stream = client2.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine(maze.ToJSON());
            writer.Flush();
        }
        public Maze GetMaze()
        {
            return this.maze;
        }
        public TcpClient GetOpponent(TcpClient client)
        {
            if (client == client1)
            {
                return this.client2;
            }
            else
            {
                return this.client1;
            }
        }
    }
}
