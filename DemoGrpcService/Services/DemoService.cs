using DemoGrpcServiceDemo;
using Grpc.Core;

namespace DemoGrpcService.Services
{
    public class DemoService : Demo.DemoBase
    {
        public override Task<HelloReply> PrintTime(HelloRequest request, ServerCallContext context)
        {
            var age = request.Age;
            var address = request.Address;
            string message = $"{DateTime.Now:HH:mm:ss.fffzzz} - Name = {request.Name}";
            Console.WriteLine(message);
            var result = new HelloReply { Message = message };
            return Task.FromResult(result);
        }
    }
}
