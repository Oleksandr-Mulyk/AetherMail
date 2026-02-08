var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("messaging");

builder.AddProject<Projects.AetherMail_SmtpWorker>("smtpworker")
       .WithReference(messaging);

builder.AddProject<Projects.AetherMail_Web>("web")
    .WithReference(messaging);

builder.Build().Run();
