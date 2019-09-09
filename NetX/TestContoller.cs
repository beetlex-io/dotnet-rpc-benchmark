using Netx.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NetxTestServer
{
    public class TestContoller : AsyncController, ITestServer
    {
        public static string ConnectionString = "";

        public Task<int> Add(int a, int b)
        {
            return Task.FromResult(a + b);
        }

        public async Task<World> Get()
        {
            using (var db = Npgsql.NpgsqlFactory.Instance.CreateConnection())
            {
                db.ConnectionString = ConnectionString;
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

        public async Task<IList<Fortune>> List()
        {
            var result = new List<Fortune>();
            using (var db = Npgsql.NpgsqlFactory.Instance.CreateConnection())
            {
                db.ConnectionString = ConnectionString;
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

        public Task<List<User>> List(int count)
        {
            List<User> items = new List<User>(count);
            for (int i = 0; i < count; i++)
            {
                var item = new User
                {
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

        public Task<HelloReply> SayHello(HelloRequest msg)
        => Task.FromResult(new HelloReply { Message = "Hello" + msg.Name });

        public Task<string> SayHello(string msg)
         => Task.FromResult("Hello" + msg);
    }
}
