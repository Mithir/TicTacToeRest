using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Net.Mime;
using ServiceStack.ServiceInterface.Cors;

class Program
{

    public class Hello
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

    public class HelloService : Service
    {
        public object Any(Hello request)
        {
            Console.WriteLine("A new Request for Hello came from - " + request.Name);
            return new HelloResponse { Result = "Hello, " + request.Name };
        }
    }

    //Define the Web Services AppHost
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost() : base("StarterTemplate HttpListener", typeof(HelloService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            Routes
                            .Add<Hello>("/hello")
                            .Add<Hello>("/hello/{Name}");
            SetConfig(new EndpointHostConfig
            {
                DefaultContentType = "application/json",
                GlobalResponseHeaders = new System.Collections.Generic.Dictionary<string, string> {
            { "Access-Control-Allow-Origin", "*" },
            { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
            { "Access-Control-Allow-Headers", "Content-Type" }
            }
            });
            Plugins.Add(new CorsFeature());

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
        Console.ReadKey();
    }
}