


using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.CONSTANS.KAFKA.topics;
using Intercore.shared.DTOs.Auth;
using Intercore.shared.DTOs.Core;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var kafkaHost = builder.Configuration["KafkaConfig:Host"] ?? "localhost:9092";

builder.Services.AddMassTransit(x =>
{
   x.UsingInMemory((context, cfg) =>
   {
        cfg.ConfigureEndpoints(context);
       
   });
   
   
   x.AddRider(rider =>
   {
       
       //Aqui traje un topic 
       rider.AddProducer<LoginMessages.LoginRequest>(AuthTopics.LoginRequest);       
       rider.AddProducer<RegisterMessages.RegisterRequest>(AuthTopics.RegisterUserCommand);
       
       
       //Mejoramiento 
       rider.AddProducer<EmisorMessages.EmisorRequest>(CoreTopics.Commands.CreateEmisor);
       
       
       
       
       rider.UsingKafka((context, k) =>
       {
           k.Host(kafkaHost);
       });
   });
});

builder.Services.AddOpenApi();

//Declaramos el host  de kafka 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();
app.Run();

