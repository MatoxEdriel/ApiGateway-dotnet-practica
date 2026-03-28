using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Intercore.ApiGateway.Api.Controllers;

public class TestRequestDto 
{
    
    public string Action { get; set; }
    public string data { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class TestController:ControllerBase
{
    
    //FromBody 
    [HttpPost("test-tcp")]
    public async Task<IActionResult> TestTcp(
        [FromBody] TestRequestDto request)
    {
        //dse deberia convertir siempre esto ? 
        var jsonString = JsonSerializer.Serialize(request);

        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        



    }





}