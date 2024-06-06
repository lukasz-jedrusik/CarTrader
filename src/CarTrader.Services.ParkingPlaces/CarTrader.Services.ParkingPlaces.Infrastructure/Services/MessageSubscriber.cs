using System.Text;
using System.Text.Json;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Messages;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Services
{
    public class MessageSubscriber : IMessageSubscriber
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;

        public MessageSubscriber(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory { HostName = _configuration["RabbitMq:Hostname"] };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IMessageSubscriber SubscribeMessage<TMessage>(
            string queue,
            string exchange,
            string routingKey,
            Func<TMessage, Task> handle) where TMessage : class, IMessage
        {
            _channel.ExchangeDeclare(exchange, "topic", durable: true, autoDelete: false, null);
            _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));

                await handle(message);

                _channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue, autoAck: false, consumer: consumer);

            return this;
        }

        public IMessageSubscriber RespondToRequest<TRequest, TResponse>(
            string queue,
            string exchange,
            string routingKey,
            Func<TRequest, Task<TResponse>> handleRequest
            )
            where TRequest : class, IMessageRequest
            where TResponse : class, IMessageResponse
        {
            _channel.ExchangeDeclare(exchange, "topic", durable: true, autoDelete: false, null);
            _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var request = JsonSerializer.Deserialize<TRequest>(requestJson);
                var response = await handleRequest(request);
                var responseJson = JsonSerializer.Serialize(response);

                var props = _channel.CreateBasicProperties();
                props.CorrelationId = ea.BasicProperties.CorrelationId;

                // Console.WriteLine($"[RespondToRequest] Received request with CorrelationId: {ea.BasicProperties.CorrelationId}");
                // Console.WriteLine($"[RespondToRequest] Replying to queue: {ea.BasicProperties.ReplyTo}");

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: ea.BasicProperties.ReplyTo,
                    basicProperties: props,
                    body: Encoding.UTF8.GetBytes(responseJson)
                );

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            // Console.WriteLine($"[RespondToRequest] Subscribing to queue: {queue}");
            _channel.BasicConsume(queue, autoAck: false, consumer: consumer);

            return this;
        }
    }
}