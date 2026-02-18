


using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.CONSTANS.KAFKA.topics;
using Intercore.shared.DTOs.Auth;



using MassTransit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


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
       
       
       
       
       
       rider.UsingKafka((context, k) =>
       {
           k.Host("localhost:9092");
       });
   });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();
app.Run();

