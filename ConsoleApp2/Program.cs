using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientHandler ch = new ClientHandler();
            Server s = new Server(8000, ch);
            s.Start();
            Console.ReadKey();

        }
    }
}
