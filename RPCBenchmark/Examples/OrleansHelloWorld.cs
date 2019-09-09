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

            for (int i = 0; i < 3; i++)
            {
                Clients.Add(CreateClient());
            }
        }
        static readonly List<IClusterClient> Clients = new List<IClusterClient>();

        static long mIndex;

        public static IGreeterGrains GreeterGrains()
        {
            var index = System.Threading.Interlocked.Increment(ref mIndex);
            return Clients[(int)(index % Clients.Count)].GetGrain<IGreeterGrains>(index);
        }

        static IClusterClient CreateClient()
        {
            var client = new ClientBuilder()
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

            client.Connect().GetAwaiter().GetResult();
            return client;
        }

    }

    [System.ComponentModel.Category("Hello")]
    public class Orleans_Hello : CodeBenchmark.IExample
    {

        public void Dispose()
        {
        }
        public async Task Execute()
        {
            var result = await GreeterGrains.SayHello(new HelloRequest { Name = "you" });
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }

    [System.ComponentModel.Category("Register")]
    public class Orleans_Register : CodeBenchmark.IExample
    {


        public void Dispose()
        {
        }
        public async Task Execute()
        {
            var result = await GreeterGrains.Register("henryfan", "henryfan@msn.com", "12345678", "cxo", "guangzhou"); 
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }

    [System.ComponentModel.Category("List")]
    public class Orleans_List : CodeBenchmark.IExample
    {


        public void Dispose()
        {
        }
        public async Task Execute()
        {
            var result = await GreeterGrains.List(10);
            if (result.Count < 10)
                throw new Exception("list error");
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }

    [System.ComponentModel.Category("Add")]
    public class Orleans_Add : CodeBenchmark.IExample
    {


        public void Dispose()
        {
        }
        public async Task Execute()
        {
            var result = await GreeterGrains.Add(1,2);
            if (result!=3)
                throw new Exception("result error");
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }

    [System.ComponentModel.Category("DB-GET")]
    public class Orleans_DB_GET : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }
        public async Task Execute()
        {
            var result = await GreeterGrains.Get();
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }

    [System.ComponentModel.Category("DB-List")]
    public class Orleans_DB_List : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }
        public async Task Execute()
        {
            var result = await GreeterGrains.List();
        }
        public void Initialize(Benchmark benchmark)
        {
            GreeterGrains = OrleansHandler.GreeterGrains();
        }

        IGreeterGrains GreeterGrains;
    }
}
