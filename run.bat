SET host="127.0.0.1"
start dotnet grpc\GreeterServer.dll %host%
start dotnet xrpc\XRPCServer.dll %host%
start dotnet orleans\OGreeter.Server.dll %host%
start dotnet netx\NetxService.dll %host%
