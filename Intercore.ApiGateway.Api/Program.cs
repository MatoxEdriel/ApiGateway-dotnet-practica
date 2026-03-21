


using System.Text;
using Intercore.ApiGateway.Api.Extensions;

using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });



builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("RequireValidToken", policy => policy.RequireAuthenticatedUser());

});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));



builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddHealthChecks();



var kafkaHost = builder.Configuration["KafkaConfig:Host"] ?? "localhost:9092";

builder.Services.AddMassTransit(x =>
{
   x.UsingInMemory((context, cfg) =>
   {
      
       
   });
   
   
   x.AddRider(rider =>
   {
       
    rider.AddGatewayProducers();
       //Mejoramiento 
      // rider.AddProducer<EmisorMessages.EmisorRequest>(.Commands.CreateEmisor);
      
       rider.UsingKafka((context, k) =>
       {
           k.Host(kafkaHost);
       });
   });
});



//Declaramos el host  de kafka 


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();


app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapControllers();
app.Run();

