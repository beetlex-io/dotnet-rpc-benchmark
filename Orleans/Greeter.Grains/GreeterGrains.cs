using OGreeter.IGrains;
using OGreeter.IGrains.Messages;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGreeter.Grains
{
    [StatelessWorker]
    public class GreeterGrains : Orleans.Grain, IGreeterGrains
    {
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
                ID = Guid.NewGuid().ToString("N")
            });
        }

        public Task<HelloReply> SayHello(HelloRequest request)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
