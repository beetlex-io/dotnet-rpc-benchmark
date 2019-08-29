# dotnet-rpc-benchmark
rpc component benchmarks  for dotnet
**[Code Benchmark](https://github.com/IKende/CodeBenchmarkDoc)**

### /tree/master/RPCBenchmark/Examples
client examples floader

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
## Result(2019-9-29)
* c50
![](https://github.com/IKende/dotnet-rpc-benchmark/blob/master/C50.png?raw=true)
* c100
![](https://github.com/IKende/dotnet-rpc-benchmark/blob/master/C100.png?raw=true)
* c200
![](https://github.com/IKende/dotnet-rpc-benchmark/blob/master/C200.png?raw=true)
