using System;
using System.Threading.Tasks;
using Connections.Amqp;
using EasyNetQ;
using MessageLibrary;

namespace SecondMicroservice
{
    class Program
    {
         static async Task Main(string[] args)
        {

            // create the bus
            var bus =  new EasyQAmqp("localhost", "5672", "guest", "guest");
            // respond to requests
            await bus.HandleRequest<SecondRequest, SecondResponse>(async request =>
            {
                System.Console.WriteLine($"Request recieve = {request.MessageSecondRequest} Time {DateTime.Now}");
               
                await Task.Delay(2000);
    
               System.Console.WriteLine($"Recieving");
                
                System.Console.WriteLine("Publish start");
                            await bus.Publish(new EventMessage
                                        {
                                            Text = "Event Message!" + request.MessageSecondRequest
                                        },Exchange.WithMultipleSubscribing);
                System.Console.WriteLine("Publish end");
                return new SecondResponse{ MessageSecondResponse  = request.MessageSecondRequest + " Add response"};
            });


            Console.ReadLine();
            
        }
    }
}
