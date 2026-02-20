
using Intercore.shared.DTOs.Auth;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
namespace Intercore.ApiGateway.Api.Controller;


[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly ITopicProducer<RegisterMessages.RegisterRequest> _producer;

    public AuthController(ITopicProducer<RegisterMessages.RegisterRequest> producer)
    {
        _producer = producer;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] RegisterMessages.RegisterRequest request)
    {

        await _producer.Produce(request);

        return Ok(new 
        { 
            Mensaje = "¡Mensaje disparado hacia Kafka exitosamente!", 
            EmailEnviado = request.Email 
        });
    }
    
    
}