
using Intercore.shared.DTOs.Auth;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
namespace Intercore.ApiGateway.Api.Controller;


[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly ITopicProducer<RegisterMessages.RegisterRequest> _producer;
    private readonly ITopicProducer<RecoveryMessages.RecoverPasswordRequest> _recoveryProducer;

    public AuthController(
        ITopicProducer<RegisterMessages.RegisterRequest> producer,
        ITopicProducer<RecoveryMessages.RecoverPasswordRequest> recoveryProducer)
    {
        _producer = producer;
        _recoveryProducer = recoveryProducer;
    }

    [HttpPost("recuperar-password")]
    public async Task<IActionResult> RecuperarPassword([FromBody] RecoveryMessages.RecoverPasswordRequest request)
    {
        await _recoveryProducer.Produce(request);

        return Ok(new
        {
            Mensaje = "Si el correo existe chato",
            Email = request.Email
            
        });

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