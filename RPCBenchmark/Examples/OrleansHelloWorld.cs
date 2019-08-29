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
    [System.ComponentModel.Category("RPC")]
    public class OrleansHelloWorld : CodeBenchmark.IExample
    {
        static readonly IClusterClient Client;
        static OrleansHelloWorld()
        {

            Client = new ClientBuilder()
            .UseStaticClustering(new IPEndPoint[] { new IPEndPoint(IPAddress.Parse("192.168.2.19"), 50053) })
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
            greeterGrains = Client.GetGrain<IGreeterGrains>(0);

        }
        public void Dispose()
        {
        }

        public async Task Execute()
        {
            var result = await greeterGrains.SayHello(new HelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {

        }
        private static IGreeterGrains greeterGrains;
    }
}
