using MassTransit;
using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.DTOs.Auth;
using Intercore.shared.DTOs.Core;

namespace Intercore.ApiGateway.Api.Extensions;

public static class KafkaProducerExtensions
{

    public static void AddGatewayProducers(this IRiderRegistrationConfigurator rider)
    {
        rider.AddProducer<LoginMessages.LoginRequest>(AuthTopics.LoginRequest);       
        rider.AddProducer<RegisterMessages.RegisterRequest>(AuthTopics.RegisterUserCommand);
        rider.AddProducer<RecoveryMessages.RecoverPasswordRequest>(AuthTopics.RecoverPasswordCommand);
        
    }

}