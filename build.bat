dotnet publish XRPC\XRPCServer\XRPCServer.csproj -c Release -o ..\..\public\xrpc
dotnet publish GRPC\GreeterServer\GreeterServer.csproj -c Release -o ..\..\public\grpc
dotnet publish Orleans\Greeter.Server\OGreeter.Server.csproj -c Release -o ..\..\public\orleans
dotnet publish Netx\NetxService.csproj -c Release -o ..\public\netx
dotnet publish RPCBenchmark\RPCBenchmark.csproj -c Release -o ..\public\benchmark

xcopy /y "benchmark.bat" .\public
xcopy /y "run.bat" .\public