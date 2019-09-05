using CodeBenchmark;
using System;

namespace RPCBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
                Setting.SERVER_HOST = args[0];
            Benchmark benchmark = new Benchmark();
            benchmark.Register(typeof(Program).Assembly);
            benchmark.Start();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                benchmark.OpenWeb();
            }

            benchmark.HttpApiServer.Log(BeetleX.EventArgs.LogType.Info, $"rpc server [{Setting.SERVER_HOST}]");
            Console.Read();
        }
    }
}
