namespace Infrastructure;

public static class RabbitMqConstants
{
    public const string ExchangeName = "app-exchange";
    
    //cola
    public const string AuthQueue = "auth-queue";
    
    //routing key
    public const string AuthCreated = "auth.created";
    
    public const string ProductCreated = "product.created";


}