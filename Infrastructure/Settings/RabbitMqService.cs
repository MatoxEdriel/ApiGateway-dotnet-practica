using Domain.Settings;
using RabbitMQ.Client;

namespace Infrastructure.Settings;

public class RabbitMqService: IRabbitMqService
{
    
    private IConnection _connection;



    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory()
        {

            HostName = "localhost"
        };

        _connection = await factory.CreateConnectionAsync();

    }

    public async Task SendMessage(string message)
    {
        throw new NotImplementedException();
    }
}