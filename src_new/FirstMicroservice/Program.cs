using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Connections.Amqp;
using EasyNetQ;
using MessageLibrary;

namespace FirstMicroservice
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // create the bus
            var bus = new EasyQAmqp("localhost", "5672", "guest", "guest");
            // respond to requests
            await bus.HandleRequest<FirstRequest, FirstResponse>(async request =>
            {
                System.Console.WriteLine($"Request recieve = {request.MessageFirst} Time {DateTime.Now}");

                await Task.Delay(2000);

                var requestMicr = new SecondRequest();
                requestMicr.MessageSecondRequest = request.MessageFirst + "'Request from First Microservice'";
        

                System.Console.WriteLine($"Recieving");
                var responseTask = bus.SendRequest<SecondRequest, SecondResponse>(requestMicr);


                await responseTask.ContinueWith((resp) =>
                {
                    System.Console.WriteLine($"Response back = {resp.Result.MessageSecondResponse} Time = {DateTime.Now}");
                });

                request.MessageFirst+=responseTask.Result.MessageSecondResponse;
                return new FirstResponse { MessageFirstResponse = request.MessageFirst + " Response From Microservice First" };
            });
            
            Console.ReadLine();
            //bus. Dispose();
        }



        


    }
}
