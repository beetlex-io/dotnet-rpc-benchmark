using BeetleX.EventArgs;
using EventNext;
using System;
using System.Threading.Tasks;
using XRPCModule;

namespace XRPCServer
{
    class Program
    {
        private static BeetleX.XRPC.XRPCServer mXRPCServer;

        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            if (args != null && args.Length > 0)
                host = args[0];
            mXRPCServer = new BeetleX.XRPC.XRPCServer();
            mXRPCServer.ServerOptions.LogLevel = BeetleX.EventArgs.LogType.Error;
            mXRPCServer.ServerOptions.DefaultListen.Host = host;
            mXRPCServer.ServerOptions.DefaultListen.Port = 50052;
            mXRPCServer.Register(typeof(Program).Assembly);
            mXRPCServer.Open();
            Console.Read();
        }

    }
    [Service(typeof(IGreeter))]
    public class Greeter : XRPCModule.IGreeter
    {
        public Task<HelloReply> SayHello(HelloRequest request)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
