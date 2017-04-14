using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
    {
        class Program
        {
            static void Main(string[] args)
            {
                IClientHandler ch = new ClientHandler();
            ServerConnection s = new ServerConnection(8000, ch);
                s.Start();
                Console.ReadKey();

            }
        }
    }


