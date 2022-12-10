using Common.Proto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace OneRiskServer.Services
{
    public class HelloServiceImpl : HelloService.HelloServiceBase
    {
        private DataStore _store;
        public HelloServiceImpl() 
        {
            _store = new DataStore();
        }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloResponse { Reply = "Zup - " + request.Greeting });
            //return base.SayHello(request, context);
        }

        public override async Task MultipleHello(HelloRequest request, IServerStreamWriter<HelloResponse> responseStream, ServerCallContext context)
        {
            var rng = new Random();
            var now = DateTime.UtcNow;

            var j = 0;
            while (!context.CancellationToken.IsCancellationRequested && j < 10)
            {
                await Task.Delay(500); // Gotta look busy

                var response = new HelloResponse
                {
                    Reply = "Zup - " + request.Greeting + " " + j++,
                    D1 = -98709.219,
                    P2 = "String 2",
                    P3 = "String 3",
                    P4 = "String 4",
                    P5 = "String 5",
                    P6 = "String 6",
                    P7 = "String 7",
                    P8 = "String 8",
                    P9 = "String 9",
                    P10 = "String 10",
                };
            //}

            //DataRow row;
            //for (var i = 0; i < _store.RowCount2; i++)
            //{
            //    row = _store[i];
            //    var response = new HelloResponse
            //    {
            //        Reply = "Zup - " + request.Greeting + " " + i++,
            //        D1 = Convert.ToDouble( row["D1"]),
            //    };

            //    response.P1 = row[response.P1.ToString()].ToString();

            //    //for (var j = 0; j < _store.Columns.ColumnList.Length; j++)
            //    //{
            //    //    response.P1 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P2 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P3 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P4 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P5 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P6 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P7 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P8 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P9 = row[_store.Columns.ColumnList[j]].ToString();
            //    //    response.P10 = row[_store.Columns.ColumnList[j]].ToString();
            //    //}

                await responseStream.WriteAsync(response);
            } // while

            // _logger.LogInformation("Sending WeatherData response");

            // await responseStream.WriteAsync(response);
            // }
            // return base.MultipleHello(request, responseStream, context);
        }
    }
}
