using System;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using HidSharp;
using PCSC;
using PCSC.Monitoring;
using PCSC.Reactive;
using PCSC.Reactive.Events;

namespace ConsolePCSC
{
    class Program
    {
        private static IMonitorFactory monitorFactory;
        private static IDisposable subscription;

        public static string SmartCardReaderName { get; set; }
        public static void Main()
        {

            Console.WriteLine("Listen device attached/detached events. Press any key to stop.");


            // var deviceMonitor = DeviceMonitor

            var factory = DeviceMonitorFactory.Instance;
            monitorFactory = MonitorFactory.Instance;

            var subscriptionDev = factory
                .CreateObservable(SCardScope.System)
                .Select(GetEventAsPrintableText)
                .Do(Console.WriteLine)
                .Subscribe(
                    onNext: _ => { },
                    onError: OnError);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            subscriptionDev.Dispose();
        }

        private static void OnError(Exception exception)
        {
            Console.WriteLine("ERROR: {0}", exception.Message);
        }


        private static string GetEventAsPrintableText(DeviceMonitorEvent ev)
        {
            string message = $"Event type {ev.GetType().Name}, (readers: {string.Join(", ", ev.Readers)})";
            Console.WriteLine("Event select");
            switch (ev)
            {
                case ReadersDetached detached:
                    message += "detached";
                    subscription?.Dispose();
                    break;
                case ReadersAttached attached:
                    message += "attached";
                    subscription = monitorFactory
                .CreateObservable(SCardScope.System, SmartCardReaderName)
                .Select(GetEventText)
                .Do(Console.WriteLine)
                .Subscribe(
                    onNext: _ => { },
                    onError: OnError);
                    break;
                case DeviceMonitorInitialized initialized:
                    message += "initialized";
                    SmartCardReaderName = ev.Readers.Where(n => !n.Contains("SAM")).First();
                    message += $"             {SmartCardReaderName}";
                    subscription = monitorFactory
                                    .CreateObservable(SCardScope.System, SmartCardReaderName)
                                    .Select(GetEventText)
                                    .Do(Console.WriteLine)
                                    .Subscribe(
                                        onNext: _ => { },
                                        onError: OnError);

                    break;

            }
            return message;
        }

        private static string GetEventText(MonitorEvent ev)
        {
            var sb = new StringBuilder();
            sb.Append($"{ev.GetType().Name} - reader: {ev.ReaderName}");
            switch (ev)
            {
                case CardStatusChanged changed:
                    sb.AppendLine($", previous state: {changed.PreviousState}, new state: {changed.NewState}");
                    break;
                case CardRemoved removed:
                    sb.AppendLine($", state: {removed.State}");
                    break;
                case CardInserted inserted:
                    sb.AppendLine($", state: {inserted.State}, ATR: {BitConverter.ToString(inserted.Atr)}");
                    break;
                case MonitorInitialized initialized:
                    sb.AppendLine($", state: {initialized.State}, ATR: {BitConverter.ToString(initialized.Atr)}");
                    break;
                case MonitorCardInfoEvent infoEvent:
                    sb.AppendLine($", state: {infoEvent.State}, ATR: {BitConverter.ToString(infoEvent.Atr)}");
                    break;
            }

            return sb.ToString();
        }


    }
}
