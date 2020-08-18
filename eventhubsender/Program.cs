using System.Security.AccessControl;
using System;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;

namespace eventhubsender
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://weatherreports.servicebus.windows.net/;SharedAccessKeyName=SenderPolicy;SharedAccessKey=5FF+YehClaHhXuIlOhvotmFqgv/+AHK7Vugj9Epv/To=";
        private const string eventHubName = "weatherhub";
        static async Task Main()
        {
            // The example code in the Skillpipe is from the legacy Microsoft.Azure.EventHubs package
            // This example is from https://docs.microsoft.com/en-us/azure/event-hubs/get-started-dotnet-standard-send-v2
            // and uses the more up to date Azure.Messaging.EventHubs package
            // Another good reference source https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/eventhub/Azure.Messaging.EventHubs/README.md

            // Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("First event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third event")));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 3 events has been published.");
            }
        }
    }
}
