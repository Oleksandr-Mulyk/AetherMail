using AetherMail.SmtpWorker.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddRabbitMQClient("messaging");

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SendEmailConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("messaging"));

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();
