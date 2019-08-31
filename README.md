# dotnet-rpc-benchmark
rpc component benchmarks  for dotnet
**[Code Benchmark](https://github.com/IKende/CodeBenchmarkDoc)**
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
    }
```
## Hello example
**client request**
```csharp
SayHello(new HelloRequest { Name = "you" });
```
**server response**
```csharp
return new HelloReply { Message = "Hello " + request.Name };
```