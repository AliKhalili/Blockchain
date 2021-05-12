﻿using System;
using System.Net;

namespace SHPA.Blockchain.SimpleServer
{
    public class SimpleServerOptions
    {
        public SimpleListenOptions CodeBackedListenOption { get; private set; }
        public IServiceProvider ApplicationServices { get; set; }


        public void Listen(IPAddress address, int port)
        {
            CodeBackedListenOption = new SimpleListenOptions(new IPEndPoint(address, port));
        }
    }
}