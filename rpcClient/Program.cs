using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Net.Security;
using System.Threading.Tasks;

namespace rpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("gRPC Client 시작 - 아무 키나 누르세요...");
            Console.ReadKey();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;   // 옛날 방식

            var channel = GrpcChannel.ForAddress("https://localhost:7185",
                new GrpcChannelOptions { HttpHandler = handler });

            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "구버전 클라" });
            Console.WriteLine(reply.Message);

            Console.WriteLine("아무 키나 눌러 종료...");
            Console.ReadKey();
        }
    }
}