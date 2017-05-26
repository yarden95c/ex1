using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ClientWpf
{
    /// <summary>
    /// 
    /// </summary>
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
        private Queue<string> commands;
        static private Queue<string> answersFromServer;
        bool startMultyPlayerGame;
        public delegate void otherPlayerMove(string direction);
        public event otherPlayerMove EventOtherPlayerMove;
        public delegate void NotConnect();
        public event NotConnect NotConnectWithServer;
        int exception;
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public Client()
        {
            this.port = Properties.Settings.Default.ServerPort;
            ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.ServerIP), this.port);
            startMultyPlayerGame = false;
            this.commands = new Queue<string>();
            answersFromServer = new Queue<string>();
        }
        /// <summary>
        /// Connects with the server
        /// </summary>
        public void Connect()
        {
            startMultyPlayerGame = false;
            isOnline = false;
            Action action = new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        string result = reader.ReadLine();
                        if (result.Contains("Name"))
                        {
                            Console.WriteLine("result: " + result);
                        }
                        if (result.Contains(this.exitGame))
                        {
                            startMultyPlayerGame = false;
                            result = this.ChecResult(result, this.exitGame);
                            isOnline = false;
                            client.Close();
                            break;
                        }
                        if (result.Contains(this.closeConnection))
                        {
                            result = this.ChecResult(result, this.closeConnection);
                            if (!startMultyPlayerGame)
                            {
                                isOnline = false;
                                client.Close();
                                break;
                            }
                            // continue;
                        }
                        if (result.Contains(this.keepOpen))
                        {
                            startMultyPlayerGame = true;
                            result = this.ChecResult(result, this.keepOpen);
                            //  continue;
                        }

                        if (result != "")
                        {
                            if (result.Contains("Direction"))
                            {
                                JObject jObject = JObject.Parse(result);
                                JToken jSolution = jObject["Direction"];
                                string move = (string)jSolution;
                                this.EventOtherPlayerMove?.Invoke(move);
                            }
                            answersFromServer.Enqueue(result.ToString());
                            Console.WriteLine("Client: " + result);
                        }
                    }

                    catch (Exception)
                    {

                        isOnline = false;
                        client.Close();
                    }
                }
            });
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (this.commands.Count > 0)
                        {
                            exception = 0;
                            string input = this.commands.Dequeue();
                            Console.WriteLine(input);
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
                            exception = 1;
                            writer.WriteLine(input);
                            writer.Flush();
                            Thread.Sleep(300);
                        }
                        else
                        {
                            Thread.Sleep(300);
                        }
                    }
                    catch (Exception)
                    {
                        if (client != null)
                        {
                            if (client.Client == null && exception == 0)
                            {
                                exception = -1;
                            }
                            else if ((exception == 0 && !client.Connected))
                            {
                                exception = -1;
                            }
                            isOnline = false;
                            client.Close();
                        }
                        this.NotConnectWithServer();
                    }
                }
            });
            exception = 0;
            thread.Start();
            while (exception == 0)
            {
                Thread.Sleep(50);
            }
            if (exception == -1)
            {
                throw new Exception();
            }

        }


        /// <summary>
        /// Checs the result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="substring">The substring.</param>
        public string ChecResult(string result, string substring)
        {
            while (result.Contains(substring) && result != substring)
            {
                int index = result.IndexOf(substring);

                if (index >= 0)
                {
                    result = result.Remove(index, substring.Length);
                }
            }
            if (!(result.Contains(closeConnection) || result.Contains(exitGame) || result.Contains(keepOpen)))
            {
                Console.WriteLine(result);
            }
            return result;

        }
        public void AddCommand(string command)
        {
            this.commands.Enqueue(command);
        }
        public string GetAnswer()
        {
            while (true)
            {
                string answer;
                if (answersFromServer.Count == 0)
                {
                    Thread.Sleep(300);
                }
                else
                {
                    answer = answersFromServer.Dequeue();
                    if (answer.Contains("Error") && answersFromServer.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        return answer;
                    }
                }
            }
        }
        /// <summary>
        /// Commands the count.
        /// </summary>
        /// <returns></returns>
        public int CommandCount()
        {
            return answersFromServer.Count;
        }
        public bool StartMultyPlayerGame
        {
            get
            {
                return this.startMultyPlayerGame;
            }
        }
        /*public void Clear()
        {
            answersFromServer.Clear();
        }*/
    }
}
