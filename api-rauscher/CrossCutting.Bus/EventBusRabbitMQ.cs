
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System;
using Domain.Core.Bus;
using Domain.Core.Events;

namespace CrossCutting.Bus
{
    public sealed class EventBusRabbitMQ : IEventBusRabbitMQ
    {
        public Task Publish<T>(T @event) where T : Event
        {
            PublisherConfig(out IConnection connection, out IModel model);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var props = model.CreateBasicProperties();
            props.Headers = new Dictionary<string, Object>() {
                {"content-type","application/json" },
                {"event-type", @event.MessageType }
            };

            props.DeliveryMode = 2;

            model.BasicPublish(exchange: "helpdesk.exchange",
                      routingKey: string.Empty,
                      basicProperties: props,
                      body: body);

            return Task.CompletedTask;
        }

        private static void PublisherConfig(out IConnection connection, out IModel model)
        {
            ConnectionFactory connectionFactory = new()
            {
                HostName = "VM-STKSERVICES",
                VirtualHost = "SiStockler",
                UserName = "sistockler",
                Password = "SiS@2020",
                Port = 5672
            };

            connection = connectionFactory.CreateConnection();
            model = connection.CreateModel();
            model.ConfirmSelect();

            model.ExchangeDeclare(exchange: ".exchange", type: "fanout", durable: true, autoDelete: false, arguments: null);
            model.QueueDeclare(queue: ".email.queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            model.QueueBind(queue: ".email.queue", exchange: ".exchange", routingKey: string.Empty, arguments: null);
        }
    }
}
