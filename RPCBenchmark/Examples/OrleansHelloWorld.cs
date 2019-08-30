using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans;
using Orleans.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using OGreeter.Grains;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using CodeBenchmark;
using System.Threading.Tasks;
using OGreeter.IGrains;
using OGreeter.IGrains.Messages;

namespace RPCBenchmark.Examples
{

    class OrleansHandler
    {
        
        static OrleansHandler()
        {
            Client = new ClientBuilder()
          .UseStaticClustering(new IPEndPoint[] { new IPEndPoint(IPAddress.Parse(Setting.SERVER_HOST), 50053) })
          .Configure<ClusterOptions>(options =>
          {
              options.ClusterId = "dev";
              options.ServiceId = "HelloWorldApp";
          })
          .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IGreeterGrains).Assembly))
          .ConfigureLogging(builder =>
          {
              builder.AddConsole();
              builder.SetMinimumLevel(LogLevel.Error);
          })
          .Build();

            Client.Connect().GetAwaiter().GetResult();
            GreeterGrains = Client.GetGrain<IGreeterGrains>(0);
        }
        public static readonly IClusterClient Client;

        public static readonly IGreeterGrains GreeterGrains;

    }

    [System.ComponentModel.Category("RPC")]
    public class Orleans_HelloWorld : CodeBenchmark.IExample
    {
        
        static Orleans_HelloWorld()
        {

          

        }
        public void Dispose()
        {
        }

        public async Task Execute()
        {
            var result = await OrleansHandler.GreeterGrains.SayHello(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {

        }
     
    }
}
