﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Entities.Interfaces;
using Funq;
using ServiceStack;
using TicTacToe.BL;
using TicTacToe.DAL;
using TicTacToe.Services;
using TicTacToe.SignalR;

namespace TicTacToe
{
    class Program
    {
        //Define the Web Services AppHost
        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost() : base("StarterTemplate HttpListener", typeof(GameService).Assembly) { }

            public override void Configure(Container container)
            {
                SetConfig(new HostConfig 
                {
                    DefaultContentType = "application/json",
                    GlobalResponseHeaders = new Dictionary<string, string> {
                        { "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" }
                    }
                });
                Plugins.Add(new CorsFeature());

                //container.Register<IGameRepository>(ct => new GameRepository());
                GameRepository repo = new GameRepository();
                repo.SubscribeToStoreEvent(new Broadcaster().Handler);
                container.Register<IGameCenter>(ctx => new GameCenter(repo));
            

            }
        }

        //Run it!
        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://localhost:1337/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}", DateTime.Now, listeningOn);

            Console.WriteLine("Trying to Activate Redis");
            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName =
                        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "redis-server.exe")
                };
                //start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                Process process = new Process {StartInfo = start};
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Redis Activation Failed - "+ex.Message);
            }
            Console.WriteLine("Redis Activation Succeeded");

            Console.WriteLine("Trying to Activate SignalR");
            try
            {
                Initiator init = new Initiator();
                init.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SignalR Activation Failed - " + ex.Message);
            }

            Console.WriteLine("SignalR Activation Succeeded");


            Console.ReadKey();
        }
    }
}