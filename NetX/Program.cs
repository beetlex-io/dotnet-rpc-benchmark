using NetxTestServer;
using System;
using System.Reflection;

namespace NetxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            if (args != null && args.Length > 0)
                host = args[0];
            string dbHost = "127.0.0.1";
            if (args != null && args.Length > 1)
                dbHost = args[1];
            Console.WriteLine($"Server Host:{host}| DB Host:{dbHost}");
            TestContoller.ConnectionString = $"Server={dbHost};Database=hello_world;User Id=benchmarkdbuser;Password=benchmarkdbpass;Maximum Pool Size=256;NoResetOnClose=true;Enlist=false;Max Auto Prepare=3";
            var service = new Netx.Service.Builder.NetxServBuilder()
                .RegisterService(Assembly.GetExecutingAssembly())
                  .ConfigNetWork(p =>
                  {
                      p.Host = host;
                      p.MaxConnectCout = 100;
                      p.Port = 50054;
                  })
                  .ConfigObjFormat(()=>new NetxSerializes.JSONSerializes())
                  .ConfigBase(p =>
                  {
                      p.VerifyKey = "11111";
                      p.ServiceName = "hellowordservice";
                  })
                  .Build();

            service.Start();
            Console.ReadLine();
        }
    }
}
