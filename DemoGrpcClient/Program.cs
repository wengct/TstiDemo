using DemoGrpcServiceDemo;
using Grpc.Net.Client;

namespace DemoGrpcClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (Console.ReadLine() != "z")
            {
                using var channel = GrpcChannel.ForAddress("https://localhost:7052");
                Demo.DemoClient client = new Demo.DemoClient(channel);
                HelloRequest helloRequest = new HelloRequest()
                {
                    Name = Guid.NewGuid().ToString(),
                    Address = "localhost",
                    Age = 18
                };
                HelloReply response = client.PrintTime(helloRequest);

                Console.WriteLine(response.Message);
            }
        }
    }
}
