namespace Domain.Settings;

public interface IRabbitMqService
{
    Task SendMessage(string message);
    
}