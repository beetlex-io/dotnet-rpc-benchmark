using OGreeter.Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;
using System.Threading.Tasks;
using OGreeter.IGrains;
using System;

namespace OGreeter.Server
{
    class Program
    {
        static Task Main(string[] args)
        {
            string host = "127.0.0.1";
            if (args != null && args.Length > 0)
                host = args[0];
            string dbHost = "127.0.0.1";
            if (args != null && args.Length > 1)
                dbHost = args[1];
            Console.WriteLine($"Server Host:{host}| DB Host:{dbHost}");
            GreeterGrains.ConnectionString = $"Server={dbHost};Database=hello_world;User Id=benchmarkdbuser;Password=benchmarkdbpass;Maximum Pool Size=256;NoResetOnClose=true;Enlist=false;Max Auto Prepare=3";
            IPAddress ip = IPAddress.Parse(host);
            return new HostBuilder()
            .UseOrleans(builder =>
            {
                builder
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "HelloWorldApp";
                    })
                    .Configure<EndpointOptions>(options =>
                    {
                        options.GatewayListeningEndpoint = new IPEndPoint(ip, 50053);
                        options.GatewayPort = options.GatewayListeningEndpoint.Port;
                        options.SiloListeningEndpoint = new IPEndPoint(ip, 11111);
                        options.SiloPort = options.SiloListeningEndpoint.Port;
                        options.AdvertisedIPAddress = options.SiloListeningEndpoint.Address;

                    })
                    .ConfigureApplicationParts(parts =>
                        parts.AddApplicationPart(typeof(GreeterGrains).Assembly).WithReferences());
                    })
                   .ConfigureServices(services =>
                   {
                       services.Configure<ConsoleLifetimeOptions>(options =>
                       {
                           options.SuppressStatusMessages = true;
                       });
                   })
                   .ConfigureLogging(builder =>
                   {
                       builder.AddConsole();
                       builder.SetMinimumLevel(LogLevel.Warning);
                   })
                   .RunConsoleAsync();
        }
    }
}
