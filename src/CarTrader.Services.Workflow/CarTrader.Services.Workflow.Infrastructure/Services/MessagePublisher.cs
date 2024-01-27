using System.Text;
using System.Text.Json;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using RabbitMQ.Client;

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
    }
}