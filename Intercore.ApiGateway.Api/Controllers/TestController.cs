using System.Text.Json;
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

    [HttpPost("product")]
    public async Task<IActionResult> Create([FromBody] ProductDto request)
    {
        try
        {

            var factory = new ConnectionFactory()
            {

                HostName = "localhost",
            };



        }
        catch (Exception e)
        {

        }
        
    }
    
}