using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Client
    {
        private int port;
        private bool isOnline;
        private IPEndPoint ep;
        private TcpClient client = null;
        private NetworkStream stream = null;
        private StreamReader reader = null;
        private StreamWriter writer = null;
        private string closeConnection = "close connection";
        private string keepOpen = "keep open";
        private string exitGame = "exit";

        Mutex startPlay = new Mutex();
        
bool startMultyPlayerGame;

        
        public Client(int port)
        {
            this.port = port;
            isOnline = false;
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.port);
            startMultyPlayerGame = false;
        }

        public void Connect()
        {
            Action action = new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        string result = reader.ReadLine();
                        if (result.Contains(this.exitGame))
                        {
                            startMultyPlayerGame = false;
                            this.ChecResult(result, this.exitGame);
                            isOnline = false;
                            client.Close();
                            break;
                        }
                        if (result.Contains(this.closeConnection))
                        {
                            this.ChecResult(result, this.closeConnection);
                            if(!startMultyPlayerGame)
                            {
                                isOnline = false;
                                client.Close();
                                break;
                            }
                            continue;
                        }
                        if (result.Contains(this.keepOpen))
                        {
                            startMultyPlayerGame = true;
                            this.ChecResult(result, this.keepOpen);
                            continue;
                        }
                        
                        if (result != "")
                        {
                            Console.WriteLine(result);
                        }
                    }

                    catch (Exception)
                    {
                        isOnline = false;
                        client.Close();
                    }
                }
            });
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Console.Write("Please enter a command: \n");
                        string input = Console.ReadLine();
                        if (!isOnline)
                        {
                            client = new TcpClient();
                            client.Connect(ep);
                            stream = client.GetStream();
                            reader = new StreamReader(stream);
                            writer = new StreamWriter(stream);
                            isOnline = true;
                            new Task(action).Start();
                        }
                        writer.WriteLine(input);
                        writer.Flush();
                        Thread.Sleep(300);
                    }
                    catch (Exception)
                    {
                        isOnline = false;
                        client.Close();
                    }
                }
            }).Start(); 
            
        }
        public void ChecResult(string result, string substring)
        {
            while (result.Contains(substring) && result != substring)
            {
                int index = result.IndexOf(substring);
              
                    if (index >= 0)
                    {
                    result = result.Remove(index, substring.Length);
                    }
            }
            if(!(result.Contains(closeConnection) || result.Contains(exitGame) || result.Contains(keepOpen)))
            {
                Console.WriteLine(result);
            }

        }

        static void Main(string[] args)
        {
            Client c = new Client(8000);
            c.Connect();
        }

    }
}
