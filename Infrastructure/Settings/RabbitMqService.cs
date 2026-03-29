using System.Text;
using System.Text.Json;
using Domain.Settings;
using RabbitMQ.Client;

namespace Infrastructure.Settings;

public class RabbitMqService: IRabbitMqService,IAsyncDisposable
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
        _channel = await _connection.CreateChannelAsync();


        await _channel.ExchangeDeclareAsync(
            exchange: RabbitMqConstants.ExchangeName,
            type: ExchangeType.Direct,
            durable:true
        );



    }

    public async Task SendMessageAsync<T>(T message, string routingKey)
    {
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        await _channel.BasicPublishAsync(
            exchange:RabbitMqConstants.ExchangeName,
            routingKey:routingKey,
            body:body
        );
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_channel is null && _connection is null) return;

        if (_channel is not null) await _channel.CloseAsync();
        if (_connection is not null) await _connection.CloseAsync();
    }
}