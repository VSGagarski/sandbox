using System;
using EasyNetQ;
using Message;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var input = "";
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                    var response = bus.Request<Request,Response>(new Request
                    {
                        Text = input
                    });
                    System.Console.WriteLine(response.Text);
                }
            }
        }
    }
}
