using System;
using System.Threading;
using EasyNetQ;
using Message;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var bus1 = RabbitHutch.CreateBus("host=localhost");
            var bus2 = RabbitHutch.CreateBus("host=localhost");
            var message = "empty";
            bus1.Subscribe<EventMessage>("test", (mes) => message = mes.Text);

            bus2.Publish<EventMessage>(new EventMessage { Text = "Hello" });
            Thread.Sleep(2000);
            Assert.Equal("Hello", message);



        }
    }
}
