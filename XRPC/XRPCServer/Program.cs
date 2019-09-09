using BeetleX.EventArgs;
using EventNext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using XRPCModule;

namespace XRPCServer
{
    class Program
    {
        private static BeetleX.XRPC.XRPCServer mXRPCServer;

        public static string ConnectionString = "";

        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            if (args != null && args.Length > 0)
                host = args[0];
            string dbHost = "192.168.2.19";
            if (args != null && args.Length > 1)
                dbHost = args[1];
           
            ConnectionString = $"Server={dbHost};Database=hello_world;User Id=benchmarkdbuser;Password=benchmarkdbpass;Maximum Pool Size=256;NoResetOnClose=true;Enlist=false;Max Auto Prepare=3";
            mXRPCServer = new BeetleX.XRPC.XRPCServer();
            mXRPCServer.ServerOptions.LogLevel = BeetleX.EventArgs.LogType.Error;
            mXRPCServer.ServerOptions.DefaultListen.Host = host;
            mXRPCServer.ServerOptions.DefaultListen.Port = 50052;
            mXRPCServer.Register(typeof(Program).Assembly);
            mXRPCServer.Open();
            mXRPCServer.Log(BeetleX.EventArgs.LogType.Info,$"Server Host:{host}| DB Host:{dbHost}");
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

        public Task<int> Add(int a,int b)
        {
            return Task.FromResult(a + b);
        }
        [ThreadInvoke(ThreadType.ThreadPool)]
        public async Task<World> Get()
        {
            using (var db =Npgsql.NpgsqlFactory.Instance.CreateConnection())
            {
                db.ConnectionString = Program.ConnectionString;
                var cmd = db.CreateCommand();
                cmd.CommandText = "SELECT id, randomnumber FROM world WHERE id = @Id";
                var id = cmd.CreateParameter();
                id.ParameterName = "@Id";
                id.DbType = DbType.Int32;
                id.Value = new Random().Next(1, 10001);
                cmd.Parameters.Add(id);
                await db.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    await rdr.ReadAsync();
                    return new World
                    {
                        Id = rdr.GetInt32(0),
                        RandomNumber = rdr.GetInt32(1)
                    };
                }
            }
        }
        [ThreadInvoke(ThreadType.ThreadPool)]
        public async Task<IList<Fortune>> ListDB()
        {
            var result = new List<Fortune>();
            using (var db = Npgsql.NpgsqlFactory.Instance.CreateConnection())
            {
                db.ConnectionString = Program.ConnectionString;
                var cmd = db.CreateCommand();
                cmd.CommandText = "SELECT id, message FROM fortune";
                var id = cmd.CreateParameter();
                id.ParameterName = "@Id";
                id.DbType = DbType.Int32;
                id.Value = new Random().Next(1, 10001);
                cmd.Parameters.Add(id);
                await db.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        result.Add(new Fortune
                        {
                            Id = rdr.GetInt32(0),
                            Message = rdr.GetString(1)
                        });
                    }
                }
            }
            return result;
        }
    }
}
