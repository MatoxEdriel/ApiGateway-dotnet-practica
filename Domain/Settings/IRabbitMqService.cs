namespace Domain.Settings;

public interface IRabbitMqService
{
    Task SendMessageAsync<T>(T message, string queueName);
    
    Task InitializeAsync();
    
}