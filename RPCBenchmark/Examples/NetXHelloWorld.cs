using CodeBenchmark;
using Netx;
using Netx.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPCBenchmark.Examples
{
    public class NetxHandler
    {
        public NetxHandler()
        {
            Client = new NetxSClientBuilder()
                .ConfigConnection(p =>
                {
                    p.Host = Setting.SERVER_HOST;
                    p.Port = 50054;
                    p.VerifyKey = "11111";
                    p.ServiceName = "hellowordservice";
                    p.RequestTimeOut = 0;
                })
                .Build();
            Greeter = Client.Get<ITestServer>();
        }

        public INetxSClient Client { get; private set; }

        public ITestServer Greeter { get; private set; }

        private static NetxHandler mSingle;

        public static NetxHandler Single
        {
            get
            {
                if (mSingle == null)
                    mSingle = new NetxHandler();
                return mSingle;
            }
        }
    }
    [System.ComponentModel.Category("RPC")]
    public class Netx_HelloWorld : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            await _greeter.SayHello(new NetxHelloRequest { Name = "you" });
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = NetxHandler.Single.Greeter;
        }

        private ITestServer _greeter;
    }


    [Build]
    public interface ITestServer
    {
        [TAG(1000)]
        Task<NetxHelloReply> SayHello(NetxHelloRequest msg);
    }

    public class NetxHelloRequest
    {
        public string Name { get; set; }
    }

    public class NetxHelloReply
    {
        public string Message { get; set; }
    }
}
