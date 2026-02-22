


using Intercore.ApiGateway.Api.Extensions;

using MassTransit;


var builder = WebApplication.CreateBuilder(args);

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

app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapControllers();
app.Run();

