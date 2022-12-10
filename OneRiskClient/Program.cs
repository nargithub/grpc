using Common.Proto;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OneRiskClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:15001");
            var client = new HelloService.HelloServiceClient(channel);

            // Hello.Greeter.GreeterClient client = new GrpcServer.Greeter.GreeterClient(channel);

            var request = new HelloRequest
            {
                Greeting = "Narendra!"
            };

            var response = await client.SayHelloAsync(request);
            Console.WriteLine($"Response from server: {response.Reply}");

            request = new HelloRequest
            {
                Greeting = "Narendra dewd!"
            };

            // response = await client.MultipleHello(request);
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            using var streamingCall = client.MultipleHello(request, cancellationToken: cts.Token);

            try
            {
                //await foreach (var calldata in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                //{
                //    Console.WriteLine(calldata.Reply);
                //}
                Console.WriteLine("Done");
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }

            Console.ReadLine();
        }
    }
}
