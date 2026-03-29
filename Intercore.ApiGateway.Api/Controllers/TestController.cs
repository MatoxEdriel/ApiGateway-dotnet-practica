using System.Text.Json;
using Domain.Settings;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Intercore.ApiGateway.Api.Controllers;

public class ProductDto 
{
    
    public string Name { get; set; }
    public string Price { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IRabbitMqService _rabbitMqService;

    
    public TestController(IRabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    [HttpPost]
    public async Task<IActionResult> create([FromBody] ProductDto dto)
    {
        await _rabbitMqService.SendMessageAsync(
            message:dto,
            routingKey: RabbitMqConstants.ProductCreated
            );

        return Accepted(
            new
            {
                
                Message = "Producto recibido y encolado para su creación.",
                State = "Procesando"
                
            });
    }




}