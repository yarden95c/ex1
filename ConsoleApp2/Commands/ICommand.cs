﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface ICommand
    {
        string Execute(string[] args, TcpClient client, string closeConnection, string keepOpen);
        bool IsValid(string[] args);
        
    }
}
