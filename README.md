# dotnet-rpc-benchmark
rpc component benchmarks  for dotnet
**[Code Benchmark](https://github.com/IKende/CodeBenchmarkDoc)**
### [Result status](https://github.com/IKende/dotnet-rpc-benchmark/tree/master/Result)
## Frameworks
-  [orleans](https://github.com/dotnet/orleans)
-  [grpc](https://github.com/grpc/grpc)
-  [xrpc](https://github.com/IKende/XRPC)
-  [NetX](https://github.com/luyikk/NetX)
## Environment
- Client E31230v2 16g
- Server E52670v2 32g
- Network 10Gb|1Gb
- System: windows
### /tree/master/RPCBenchmark/Examples
client examples folder

## Service interface
``` csharp
    public interface IGreeter
    {
        Task<HelloReply> SayHello(HelloRequest request);

        Task<User> Register(string name, string email, string password, string title, string city);

        Task<List<User>> List(int count);
        
    }
```
## Hello
**client request**
```csharp
SayHello(new HelloRequest { Name = "you" });
```
**server response**
```csharp
return new HelloReply { Message = "Hello " + request.Name };
```
## Register
**client request**
```csharp
Greeter.Register("henryfan", "henryfan@msn.com", "12345678", "cxo", "guangzhou");
```
**server response**
```csharp
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
```
## List
**client request**
```csharp
Greeter.List(10);
```
**server response**
```csharp
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
```
