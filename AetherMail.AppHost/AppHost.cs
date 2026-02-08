var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("messaging");

var mailServer = builder.AddContainer("mailserver", "axllent/mailpit")
    .WithEndpoint(targetPort: 1025, port: 1025, name: "smtp")
    .WithHttpEndpoint(targetPort: 8025, port: 8025, name: "webui");

builder.AddProject<Projects.AetherMail_SmtpWorker>("smtpworker")
       .WithReference(messaging)
       .WithReference(mailServer.GetEndpoint("smtp"));

builder.AddProject<Projects.AetherMail_Web>("web")
    .WithReference(messaging);

builder.Build().Run();
