using System.Text;
using System.Text.Json;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CarTrader.Services.Workflow.Infrastructure.Services
{
    public class MessagePublisher(IConnection connection) : IMessagePublisher
    {
        private readonly IConnection _connection = connection;

        public async Task PublishMessageAsync<TMessage>(string exchange, string routingKey, TMessage message)
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange, "topic", true, false);
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: body);

            await Task.CompletedTask;
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(
            string exchange,
            string routingKey,
            TRequest request,
            Func<TResponse, Task> handleResponse)
        {
            using var channel = _connection.CreateModel();

            var jsonRequest = JsonSerializer.Serialize(request);
            var body = Encoding.UTF8.GetBytes(jsonRequest);

            var replyQueueName = channel.QueueDeclare().QueueName;
            var correlationId = Guid.NewGuid().ToString();

            var props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            var responseTaskCompletionSource = new TaskCompletionSource<TResponse>();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId != correlationId)
                {
                    return;
                }

                var jsonResponse = Encoding.UTF8.GetString(ea.Body.ToArray());
                var response = JsonSerializer.Deserialize<TResponse>(jsonResponse);
                await handleResponse(response);
                responseTaskCompletionSource.SetResult(response);
            };

            channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

            channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: props,
                body: body
            );

            return await responseTaskCompletionSource.Task;
        }
    }
}