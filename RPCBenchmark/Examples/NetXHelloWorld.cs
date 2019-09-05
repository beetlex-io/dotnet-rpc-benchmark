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
        static NetxHandler()
        {
            for(int i=0;i<3;i++)
            {
               var client = new NetxSClientBuilder()
               .ConfigConnection(p =>
               {
                   p.Host = Setting.SERVER_HOST;
                   p.Port = 50054;
                   p.VerifyKey = "11111";
                   p.ServiceName = "hellowordservice";
                   p.RequestTimeOut = 0;
               })
               .ConfigObjFormat(()=>new NetxSerializes.JSONSerializes())
               .Build();
                mClients.Add(client);
            }
        }

        private static long mIndex;

        public static ITestServer GetClient()
        {
            long index = System.Threading.Interlocked.Increment(ref mIndex);
            return mClients[(int)(index % mClients.Count)].Get<ITestServer>();
        }


        private static List<NetxSClient> mClients = new List<NetxSClient>();

       
    }
    [System.ComponentModel.Category("Hello")]
    public class Netx_Hello : CodeBenchmark.IExample
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
            _greeter = NetxHandler.GetClient();
        }

        private ITestServer _greeter;
    }

    [System.ComponentModel.Category("Register")]
    public class Netx_Register : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            await _greeter.Register("henryfan", "henryfan@msn.com", "12345678", "cxo", "guangzhou");
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = NetxHandler.GetClient();
        }

        private ITestServer _greeter;
    }


    [System.ComponentModel.Category("List")]
    public class Netx_List : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var result =await _greeter.List(10);
            if (result.Count < 10)
                throw new Exception("list error");
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = NetxHandler.GetClient();
        }

        private ITestServer _greeter;
    }

    [System.ComponentModel.Category("Add")]
    public class Netx_Add : CodeBenchmark.IExample
    {
        public void Dispose()
        {

        }

        public async Task Execute()
        {
            var r = await _greeter.Add(1, 2);

            if (r != 3)
                throw new Exception("res error");
        }

        public void Initialize(Benchmark benchmark)
        {
            _greeter = NetxHandler.GetClient();
        }

        private ITestServer _greeter;
    }


    [Build]
    public interface ITestServer
    {
        [TAG(1000)]
        Task<NetxHelloReply> SayHello(NetxHelloRequest msg);

        [TAG(1001)]
        Task<User> Register(string name, string email, string password, string title, string city);

        [TAG(1002)]
        Task<List<User>> List(int count);

        [TAG(1003)]
        Task<int> Add(int a, int b);
    }


    public class User
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Title { get; set; }

        public string City { get; set; }
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
