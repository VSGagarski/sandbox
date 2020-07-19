using System;
using EasyNetQ;
using Message;

namespace Consumer
{
    class Program
    {
       static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.PubSub.Subscribe<EventMessage>($"{args[0]}", HandleTextMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(EventMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Got message: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(textMessage.Text);
            Console.ResetColor();
        }
    }
}
