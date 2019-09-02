using BeetleX.EventArgs;
using EventNext;
using System;
using System.Collections.Generic;
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
        public Task<List<User>> List(int count)
        {
            List<User> items = new List<User>(count);
            for(int i=0;i<count;i++)
            {
                var item = new User {
                    Name = "henryfan",
                    City = "guangzhou",
                    Email = "henryfan@msn.com",
                    Title = "cxo",
                    Password = "12345678",
                    ID = Guid.NewGuid().ToString("N"),
                    CreateTime = DateTime.Now
                };
                items.Add(item);
            }
            return Task.FromResult(items);
        }

        public Task<User> Register(string name, string email, string password, string title, string city)
        {
            return Task.FromResult(new User
            {
                Name = name,
                Email = email,
                Password = password,
                Title = title,
                City = city,
                CreateTime = DateTime.Now,
                ID= Guid.NewGuid().ToString("N")
            });
        }

        public Task<HelloReply> SayHello(HelloRequest request)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
