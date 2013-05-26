using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin.Hosting;

namespace TicTacToe.SignalR
{
    public class Initiator
    {
        
        public void Start()
        {
            //StartOptions so = new StartOptions { OutputFile = @"c:\amitLog.txt", Url = url };
            string url = "http://localhost:1338";

            using (WebApplication.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
