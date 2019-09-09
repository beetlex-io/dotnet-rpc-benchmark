SET host="127.0.0.1"
SET dbhost="192.168.2.19"
start dotnet grpc\GreeterServer.dll %host% %dbhost%
start dotnet xrpc\XRPCServer.dll %host% %dbhost%
start dotnet orleans\OGreeter.Server.dll %host% %dbhost%
start dotnet netx\NetxService.dll %host% %dbhost%
