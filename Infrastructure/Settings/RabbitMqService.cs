using System.Text;
using System.Text.Json;
using Domain.Settings;
using RabbitMQ.Client;

namespace Infrastructure.Settings;

public class RabbitMqService: IRabbitMqService
{
    
    private IConnection _connection;

    private IChannel _channel;


    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory()
        {

            HostName = "localhost"
        };

        _connection = await factory.CreateConnectionAsync();

    }

    public async Task SendMessageAsync<T>(T message, string queueName)
    {
        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        await _channel.BasicPublishAsync(
            //definir que tipo de exchange usar mas tarde xd 
            exchange: string.Empty, 
            routingKey: queueName,  // 
            body: body);
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_channel is null && _connection is null) return;

        if (_channel is not null) await _channel.CloseAsync();
        if (_connection is not null) await _connection.CloseAsync();
    }
}