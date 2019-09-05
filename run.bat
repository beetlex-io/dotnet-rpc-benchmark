SET host="127.0.0.1"
start dotnet public\grpc\GreeterServer.dll %host%
start dotnet public\xrpc\XRPCServer.dll %host%
start dotnet public\orleans\OGreeter.Server.dll %host%
start dotnet public\netx\NetxService.dll %host%
