# TODO: Parametrize URL and name 
iwr https://localhost:44351/swagger/v1/swagger.json -o Alpha.Travel.WebApi.v1.json
autorest --input-file=Alpha.Travel.WebApi.v1.json --csharp --output-folder=Alpha.Travel.WebApi.ClientSDK --namespace=Alpha.Travel.WebApi.ClientSDK

dotnet new sln -n Alpha.Travel.WebApi.Sdk --force
dotnet new classlib -o Alpha.Travel.WebApi.ClientSdk --force
dotnet sln Alpha.Travel.WebApi.Sdk.sln add Alpha.Travel.WebApi.ClientSdk
cd Alpha.Travel.WebApi.ClientSdk
del class1.cs

dotnet add package Microsoft.AspNetCore --version 2.1-preview2-final
dotnet add package Microsoft.Rest.ClientRuntime --version 2.3.11
dotnet add package Newtonsoft.Json --version 11.0.2

dotnet restore
dotnet build
dotnet pack --include-symbols