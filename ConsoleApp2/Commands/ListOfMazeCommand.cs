﻿using System;
using System.Net.Sockets;

namespace ConsoleApp2
{
    internal class ListOfMazeCommand : ICommand
    {
        private IModel model;

        public ListOfMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            return model.GetList().ToString();
        }
    }
}