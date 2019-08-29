using CodeBenchmark;
using System;

namespace RPCBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmark benchmark = new Benchmark();
            benchmark.Register(typeof(Program).Assembly);
            benchmark.Start();
            Console.Read();
        }
    }
}
