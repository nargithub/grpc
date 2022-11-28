using Common.Proto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace OneRiskServer.Services
{
    public class HelloServiceImpl : HelloService.HelloServiceBase
    {
        public HelloServiceImpl() { }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloResponse { Reply = "Zup - " + request.Greeting });
            //return base.SayHello(request, context);
        }

        public override async Task MultipleHello(HelloRequest request, IServerStreamWriter<HelloResponse> responseStream, ServerCallContext context)
        {
            var rng = new Random();
            var now = DateTime.UtcNow;

            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested && i < 20)
            {
                await Task.Delay(500); // Gotta look busy

                var response = new HelloResponse
                {
                    Reply = "Zup - " + request.Greeting + " " + i++
                    //DateTimeStamp = Timestamp.FromDateTime(now.AddDays(i++)),
                    //TemperatureC = rng.Next(-20, 55),
                    //Summary = Summaries[rng.Next(Summaries.Length)]
                };

                // _logger.LogInformation("Sending WeatherData response");

                await responseStream.WriteAsync(response);
            }
            // return base.MultipleHello(request, responseStream, context);
        }
    }
}
