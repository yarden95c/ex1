﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
