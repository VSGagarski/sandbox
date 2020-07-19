using System;
using System.Threading;
using System.Threading.Tasks;
using Connections.Amqp;
using EasyNetQ;
using MessageLibrary;
using Microsoft.AspNetCore.Components;

namespace FirstBlazor.Pages
{
    partial class Index
    {
        [Inject]
        public IAmqpConnection bus { get; set; }

        public FirstRequest Message { get; set; } = new FirstRequest { MessageFirst = "" };

        public string EventMessega { get; set; } = "";

        public Index()
        {
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {

            return base.SetParametersAsync(parameters);
        }

        protected override void OnInitialized()
        {

        }
        protected override void OnAfterRender(bool firstRender)
        {
            System.Console.WriteLine(firstRender.ToString());
            if (firstRender)
            {
                bus.SubscribeTo<EventMessage>(HandleTextMessage, Exchange.WithMultipleSubscribing);
                System.Console.WriteLine("SetParametrs");
            }
        }

        private Task HandleTextMessage(EventMessage obj)
        {
            InvokeAsync(() => { EventMessega = obj.Text; StateHasChanged(); });
            return Task.CompletedTask;
        }
        private async Task HandleClick()
        {

            Message.MessageFirst += $" {DateTime.Now}";
            var responseTask = bus.SendRequest<FirstRequest, FirstResponse>(Message);

            await responseTask.ContinueWith((resp) =>
            {
                System.Console.WriteLine($"Response back = {resp.Result.MessageFirstResponse} Time = {DateTime.Now}");
            });
            await Task.Run(() => System.Console.WriteLine());

        }
    }
}