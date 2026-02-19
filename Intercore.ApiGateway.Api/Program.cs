


using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.DTOs.Auth;
using Intercore.shared.DTOs.Core;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var kafkaHost = builder.Configuration["KafkaConfig:Host"] ?? "localhost:9092";

builder.Services.AddMassTransit(x =>
{
   x.UsingInMemory((context, cfg) =>
   {
      
       
   });
   
   
   x.AddRider(rider =>
   {
       
       //Aqui traje un topic 
       rider.AddProducer<LoginMessages.LoginRequest>(AuthTopics.LoginRequest);       
       rider.AddProducer<RegisterMessages.RegisterRequest>(AuthTopics.RegisterUserCommand);
       
       
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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();
app.Run();

